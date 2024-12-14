using AutoMapper;
using Business.Manager.Interface;
using DTO.Concrete.RoleDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB.Areas.Admin.Models.ViewModels.Roles;
using WEB.Areas.Admin.Models.ViewModels.Users;

namespace WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        private readonly IRoleManager _roleManager;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public RolesController(IRoleManager roleManager, IMapper mapper, IUserManager userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var dto = await _roleManager.GetRolesAsync();
            var model = _mapper.Map<List<GetRoleVM>>(dto);
            return View(model);
        }

        public IActionResult CreateRole() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleVM model)
        {
            if (ModelState.IsValid)
            {
                if (await _roleManager.CheckRoleNameAsync(model.Name))
                {
                    TempData["Error"] = "Bu rol adı kullanılmaktadır!";
                    return View(model);
                }
                var dto = _mapper.Map<CreateRoleDTO>(model);
                var result = await _roleManager.CreateRoleAsync(dto);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Rol sisteme kaydedilmiştir!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Rol sisteme kaydedilememiştir!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> UpdateRole(string id)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(id, out entityId);

            if (!guidResult)
            {
                TempData["Error"] = "Rol bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var dto = await _roleManager.FindRoleByIdAsync<UpdateRoleDTO>(entityId);
            var model = _mapper.Map<UpdateRoleVM>(dto);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRole(UpdateRoleVM model)
        {
            if (ModelState.IsValid)
            {
                if (await _roleManager.CheckRoleNameAsync(model.Name, model.Id))
                {
                    TempData["Error"] = "Bu isim kullanılmaktadır!";
                    return View(model);
                }

                var checkId = await _roleManager.AnyRoleById(model.Id);
                if (!checkId)
                {
                    TempData["Error"] = "Rol bulunamadı!";
                    return RedirectToAction(nameof(Index));
                }

                var dto = _mapper.Map<UpdateRoleDTO>(model);

                var result = await _roleManager.UpdateRoleAsync(dto);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Rol güncellendi!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Rol güncellenemedi!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(id, out entityId);

            if (!guidResult)
            {
                TempData["Error"] = "Rol bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var checkId = await _roleManager.AnyRoleById(entityId);
            if (!checkId)
            {
                TempData["Error"] = "Rol bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var checkUser = await _roleManager.CheckAnyUserInRoleAsync(entityId);

            if (checkUser)
            {
                TempData["Error"] = "Bu rolde kullanıcılar vardır. Silinemez!";
                return RedirectToAction(nameof(Index));
            }

            var result = await _roleManager.DeleteRoleAsync(entityId);
            if (result.Succeeded)
            {
                TempData["Success"] = "Rol silinmiştir!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Rol silinememiştir!";
            return Redirect(nameof(Index));
        }

        public async Task<IActionResult> AssignToRole(string id)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(id, out entityId);

            if (!guidResult)
            {
                TempData["Error"] = "Rol bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var checkId = await _roleManager.AnyRoleById(entityId);
            if (!checkId)
            {
                TempData["Error"] = "Rol bulunamadı!";
                return RedirectToAction(nameof(Index));
            }
            var dto = await _roleManager.FindRoleByIdAsync<GetRoleDTO>(entityId);
            var vm = _mapper.Map<GetRoleVM>(dto);
            var model = new AssignRoleVM
            {
                Role = vm,
                RoleName = vm.Name,
                HasRole = _mapper.Map<List<GetUserForRoleVM>>(await _userManager.GetUsersHasRoleAsync(vm.Name)),
                HasNotRole = _mapper.Map<List<GetUserForRoleVM>>(await _userManager.GetUsersHasNotRoleAsync(vm.Name))
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignToRole(AssignRoleVM model)
        {
            model.HasRole = _mapper.Map<List<GetUserForRoleVM>>(await _userManager.GetUsersHasRoleAsync(model.RoleName));
            model.HasNotRole = _mapper.Map<List<GetUserForRoleVM>>(await _userManager.GetUsersHasNotRoleAsync(model.RoleName));

            bool resultAdd = true, resultRemove = true;

            foreach (var userId in model.AddIds ?? new string[] {}) 
            {
                resultAdd = await _userManager.AddUserToRoleAsync(Guid.Parse(userId), model.RoleName);
            }

            foreach (var userId in model.RemoveIds ?? new string[] { })
            {
                resultRemove = await _userManager.RemoveUserFromRoleAsync(Guid.Parse(userId), model.RoleName);
            }

            if (resultAdd && resultRemove)
            {
                TempData["Success"] = "İşlem başarılı!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "İşlem başarısız";
            return View(model);
        }
    }
}
