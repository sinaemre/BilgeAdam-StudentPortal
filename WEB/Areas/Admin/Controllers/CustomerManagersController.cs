using ApplicationCore.Consts;
using AutoMapper;
using Business.Manager.Interface;
using DataAccess.Services.Interface;
using DTO.Concrete.CustomerManagerDTO;
using Microsoft.AspNetCore.Mvc;
using WEB.Areas.Admin.Models.ViewModels.CustomerManagers;

namespace WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerManagersController : Controller
    {
        private readonly ICMManager _customerManagerService;
        private readonly IMapper _mapper;

        public CustomerManagersController(ICMManager customerManagerService, IMapper mapper)
        {
            _customerManagerService = customerManagerService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var customerManagers = await _customerManagerService.GetFilteredListAsync
                (
                    select: x => new GetCustomerManagerVM
                    {
                        Id = x.Id,
                        FullName = x.FirstName + " " + x.LastName,
                        Email = x.Email,
                        BirthDate = x.BirthDate.ToShortDateString(),
                        HireDate = x.HireDate.ToShortDateString(),
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate != null ? x.UpdatedDate.Value.ToString("d.M.yyyy HH:mm:ss") : " - ",
                        Status = x.Status == Status.Active ? "Aktif" : "Güncellenmiş",
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate)
                );
            return View(customerManagers);
        }

        public IActionResult CreateCustomerManager() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomerManager(CreateCustomerManagerVM model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<CreateCustomerManagerDTO>(model);
                var result = await _customerManagerService.AddAsync(dto);
                if (result)
                {
                    TempData["Success"] = $"{dto.FirstName + " " + dto.LastName} müşteri yöneticisi sisteme kaydedilmiştir!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Müşteri yönetisi sisteme kayıt edilemedi!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> UpdateCustomerManager(string id)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(id, out entityId);

            if (!guidResult)
            {
                TempData["Error"] = "Müşteri yöneticisi bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var dto = await _customerManagerService.GetByIdAsync<UpdateCustomerManagerDTO>(entityId);
            if (dto != null)
            {
                var model = _mapper.Map<UpdateCustomerManagerVM>(dto);
                return View(model);
            }
            TempData["Error"] = "Müşteri yöneticisi bulunamadı!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCustomerManager(UpdateCustomerManagerVM model)
        {
            if (ModelState.IsValid) 
            {
                var dto = _mapper.Map<UpdateCustomerManagerDTO>(model);
                var result = await _customerManagerService.UpdateAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Müşteri yöneticisi güncellendi!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Müşteri yöneticisi güncellenemedi!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> DeleteCustomerManager(string id)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(id, out entityId);

            if (!guidResult)
            {
                TempData["Error"] = "Müşteri yöneticisi bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var dto = await _customerManagerService.GetByIdAsync<UpdateCustomerManagerDTO>(entityId);
            if (dto != null)
            {
                var result = await _customerManagerService.DeleteAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Müşteri yöneticisi silinmiştir!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Müşteri yöneticisi silinememiştir!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Müşteri yöneticisi bulunamadı!";
            return RedirectToAction(nameof(Index));
        }
    }
}
