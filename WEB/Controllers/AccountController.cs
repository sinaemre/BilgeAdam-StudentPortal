using AutoMapper;
using Business.Manager.Interface;
using DTO.Concrete.AccountDTO;
using DTO.Concrete.TeacherDTO;
using DTO.Concrete.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using WEB.ActionFilters;
using WEB.Areas.Admin.Controllers;
using WEB.Models.ViewModels.Account;

namespace WEB.Controllers;

public class AccountController : Controller
{
    private readonly IUserManager _userManager;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;

    public AccountController(IUserManager userManager, IMapper mapper, IEmailSender emailSender)
    {
        _userManager = userManager;
        _mapper = mapper;
        _emailSender = emailSender;
    }

    public IActionResult Login() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM model)
    {
        if (ModelState.IsValid)
        {
            var dto = _mapper.Map<LoginDTO>(model);
            var result = await _userManager.LoginAsync(dto);
            if (result)
            {
                var appUser = await _userManager.FindUserAsync<GetUserDTO>(HttpContext.User);
                if (!appUser.HasPasswordChanged)
                {
                    TempData["Error"] = "İlk kez giriş yaptığınız için Email'inize gelen linkten şifrenizi değiştirmelisiniz!";
                    await _userManager.LogoutAsync();
                    return RedirectToAction(nameof(Login));
                }

                TempData["Success"] = $"Hoşgeldiniz {model.UserName}";
                if (await _userManager.IsUserInRoleAsync(model.UserName, "admin"))
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

                if (await _userManager.IsUserInRoleAsync(model.UserName, "customerManager"))
                    return RedirectToAction("Index", "Home", new { area = "Education" });

                if (await _userManager.IsUserInRoleAsync(model.UserName, "teacher"))
                    return RedirectToAction("GetClassroomsForTeacherByTeacherId", "Classrooms", new { area = "Education"});

                if (await _userManager.IsUserInRoleAsync(model.UserName, "student"))
                    return RedirectToAction("StudentDetail", "Students", new { area = "Education" });

                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = "Kullanıcı adı veya şifre yanlış!";
            return View(model);
        }
        TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
        return View(model);
    }

    [Authorize]
    public async Task<IActionResult> EditUser()
    {
        var dto = await _userManager.FindUserAsync<EditUserDTO>(HttpContext.User);
        if (dto != null)
        {
            var model = _mapper.Map<EditUserVM>(dto);
            return View(model);
        }

        TempData["Error"] = "Kullanıcı bulunamadı!";
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(EditUserVM model)
    {
        if (ModelState.IsValid)
        {
            var dto = _mapper.Map<EditUserDTO>(model);
            if (dto != null)
            {
                var result = await _userManager.UpdateUserAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Kullanıcı bilgileri düzenlendi!";
                    return View(model);
                }
                TempData["Error"] = "Kullanıcı bilgileri düzenlenemedi!";
                return View(model);
            }
            TempData["Error"] = "Kullanıcı bulunamadı!";
            return RedirectToAction("Index", "Home");
        }
        TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
        return View(model);
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        if (HttpContext.User.Identity.IsAuthenticated)
        {
            await _userManager.LogoutAsync();
            TempData["Success"] = "Çıkış yapıldı!";
            return RedirectToAction(nameof(Login));
        }

        TempData["Error"] = "Önce giriş yapınız!";
        return RedirectToAction(nameof(Login));
    }

    [Authorize]
    public async Task<IActionResult> ChangePassword()
    {
        //HttpContext.User => Giriş yapmış kullanıcı
        var model = new ChangePasswordVM
        {
            Id = await _userManager.GetUserIdAsync(HttpContext.User)
        };
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
    {
        if (ModelState.IsValid)
        {
            var dto = _mapper.Map<ChangePasswordDTO>(model);
            var result = await _userManager.ChangePasswordAsync(dto);
            if (result)
            {
                TempData["Success"] = "Şifre başarılı bir şekilde değiştirildi!";
                return RedirectToAction(nameof(EditUser));
            }
            TempData["Error"] = "Şifre değiştirilemedi!";
            return View(model);
        }
        TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
        return View(model);
    }

    [ValidateTokenExpiryFilter]
    public IActionResult CreatePassword(string email, string token = null)
    {
        if (string.IsNullOrEmpty(token))
        {
            TempData["Error"] = "Token değeri null olamaz!";
            return BadRequest();
        }

        var model = new CreatePasswordVM { Token = token, Email = email };
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePassword(CreatePasswordVM model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindUserByEmailAsync(model.Email);

            if (user != null)
            {
                var dto = _mapper.Map<CreatePasswordDTO>(model);
                var result = await _userManager.ChangePasswordAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Şifreniz başarıyla değiştirildi. Giriş yapabilirsiniz!";
                    return RedirectToAction(nameof(Login));
                }
                TempData["Error"] = "Şifreniz değiştirilemedi!";
                return View(model);
            }
            TempData["Error"] = "Bu email'de bir kullanıcı bulunamadı!";
            return RedirectToAction(nameof(Login));
        }
        TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
        return View(model);
    }

    public IActionResult ForgotPassword() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindUserByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["Error"] = "Bu maile kayıtlı bir kullanıcı yoktur. Yöneticinizle iletişime geçebilirsiniz!";
                return RedirectToAction(nameof(Login));
            }

            var token = await _userManager.GenerateTokenForResetPassword(user.Id);

            var callbackUrl = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "Şifre Sıfırlama", $"<p>Şifrenizi sıfırlamak için <a href='{callbackUrl}'>buraya tıklayınız!</a></p>");

            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }
        TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
        return View(model);

    }
    public IActionResult ForgotPasswordConfirmation() => View();

    [ValidateTokenExpiryFilter]
    public IActionResult ResetPassword(string email, string token = null)
    {
        if (token ==  null)
        {
            TempData["Error"] = "Token zorunludur!";
            return BadRequest();
        }
        var model = new ResetPasswordVM { Token = token, Email = email };
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindUserByEmailAsync(model.Email);
            if (user != null)
            {
                var dto = _mapper.Map<ResetPasswordDTO>(model);
                var result = await _userManager.ResetPasswordAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Şifreniz başarılı bir şekilde değiştirilmiştir. Giriş yapabilirsiniz!";
                    return RedirectToAction(nameof(Login));
                }
                TempData["Error"] = "Şifreniz değiştirilemedi!";
                return View(model);
            }
            TempData["Error"] = "Kullanıcı bulunamadı!";
            return RedirectToAction(nameof(Login));
        }
        TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
        return View(model);
    }
}