﻿using ApplicationCore.Consts;
using AutoMapper;
using Business.Manager.Interface;
using DTO.Concrete.CourseDTO;
using Microsoft.AspNetCore.Mvc;
using WEB.Areas.Education.Models.ViewModels.Courses;

namespace WEB.Areas.Education.Controllers
{
    [Area("Education")]
    public class CoursesController : Controller
    {
        private readonly ICourseManager _courseManager;
        private readonly IMapper _mapper;

        public CoursesController(ICourseManager courseManager, IMapper mapper)
        {
            _courseManager = courseManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseManager.GetFilteredListAsync
                (
                    select: x => new GetCourseVM
                    {
                        Id = x.Id, 
                        Name = x.Name,
                        TotalHour = x.TotalHour != null ? x.TotalHour.Value.ToString() : " - ",
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate != null ? x.UpdatedDate.Value.ToString("d.M.yyyy HH:mm:ss") : " - ",
                        Status = x.Status == Status.Active ? "Aktif" : "Güncellenmiş"
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate)
                );

            return View(courses);
        }

        public IActionResult CreateCourse() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(CreateCourseVM model)
        {
            if (ModelState.IsValid)
            {
                var checkName = await _courseManager.AnyAsync(x => x.Name.ToLower() == model.Name.ToLower());
                if (checkName) 
                {
                    TempData["Error"] = "Bu isimde kurs bulunmaktadır!";
                    return View(model);
                }

                var dto = _mapper.Map<CreateCourseDTO>(model);
                var result = await _courseManager.AddAsync(dto);
                if (result)
                {
                    TempData["Success"] = $"{dto.Name} kursu sisteme kaydedilmiştir!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Bu kurs sisteme kayıt edilememiştir!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> UpdateCourse(string id)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(id, out entityId);
            if (!guidResult)
            {
                TempData["Error"] = "Kurs bulunamadı!";
                return RedirectToAction(nameof(Index));
            }
            var dto = await _courseManager.GetByIdAsync<UpdateCourseDTO>(entityId);
            if (dto != null)
            {
                var model = _mapper.Map<UpdateCourseVM>(dto);
                return View(model);
            }
            TempData["Error"] = "Kurs bulunamadı!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCourse(UpdateCourseVM model)
        {
            if (ModelState.IsValid) 
            {
                var checkName = await _courseManager.AnyAsync(x => x.Name == model.Name && x.Id != model.Id);
                if (checkName)
                {
                    TempData["Error"] = "Bu isim kullanılmaktadı!";
                    return View(model);
                }
                var dto = _mapper.Map<UpdateCourseDTO>(model);
                if (dto != null) 
                {
                    var result = await _courseManager.UpdateAsync(dto);
                    if (result)
                    {
                        TempData["Success"] = "Kurs güncellendi!";
                        return RedirectToAction(nameof(Index));
                    }
                    TempData["Error"] = "Kurs güncellenemedi!";
                    return View(model);
                }
                TempData["Error"] = "Kurs bulunamadı!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> DeleteCourse(string id)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(id, out entityId);
            if (!guidResult)
            {
                TempData["Error"] = "Kurs bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var dto = await _courseManager.GetByIdAsync<UpdateCourseDTO>(entityId);
            if (dto != null)
            {
                var result = await _courseManager.DeleteAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Kurs silindi!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Kurs silinemedi!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Kurs bulunamadı!";
            return RedirectToAction(nameof(Index));
        }
    }
}
