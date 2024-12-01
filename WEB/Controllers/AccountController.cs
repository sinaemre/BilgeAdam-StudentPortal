using AutoMapper;
using Business.Manager.Interface;
using DTO.Concrete.AccountDTO;
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

    public AccountController(IUserManager userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
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
                TempData["Success"] = $"Hoşgeldiniz {model.UserName}";
                if (await _userManager.IsUserInRoleAsync(model.UserName, "admin"))
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

                if (await _userManager.IsUserInRoleAsync(model.UserName, "customerManager") || await _userManager.IsUserInRoleAsync(model.UserName, "student") || await _userManager.IsUserInRoleAsync(model.UserName, "teacher"))
                    return RedirectToAction("Index", "Home", new { area = "Education" });

                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = "Kullanıcı adı veya şifre yanlış!";
            return View(model);
        }
        TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
        return View(model);
    }

    public async Task<IActionResult> EditUser()
    {
        var dto = await _userManager.FindUserAsync(HttpContext.User);
        if (dto != null)
        {
            var model = _mapper.Map<EditUserVM>(dto);
            return View(model);
        }

        TempData["Error"] = "Kullanıcı bulunamadı!";
        return RedirectToAction("Index", "Home");
    }

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
    public async Task<IActionResult> CreatePassword(string email, string token = null)
    {
        if (string.IsNullOrEmpty(token))
        {
            TempData["Error"] = "Token değeri null olamaz!";
            return BadRequest();
        }

        var model = new CreatePasswordVM { Token = token, Email = email };
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken, ValidateTokenExpiryFilter]
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
}