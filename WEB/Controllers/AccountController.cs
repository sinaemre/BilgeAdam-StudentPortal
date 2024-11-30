using AutoMapper;
using Business.Manager.Interface;
using DTO.Concrete.AccountDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
}