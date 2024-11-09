using ApplicationCore.Consts;
using ApplicationCore.Entities.Concrete;
using AutoMapper;
using Business.Manager.Interface;
using DTO.Concrete.CourseDTO;
using DTO.Concrete.TeacherDTO;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB.Areas.Education.Models.ViewModels.Courses;
using WEB.Areas.Education.Models.ViewModels.Teachers;

namespace WEB.Areas.Education.Controllers
{
    [Area("Education")]
    public class TeachersController : Controller
    {
        private readonly ITeacherManager _teacherManager;
        private readonly IMapper _mapper;
        private readonly ICourseManager _courseManager;

        public TeachersController(ITeacherManager teacherManager, IMapper mapper, ICourseManager courseManager)
        {
            _teacherManager = teacherManager;
            _mapper = mapper;
            _courseManager = courseManager;
        }

        public async Task<IActionResult> Index()
        {
            var teachers = await _teacherManager.GetFilteredListAsync
                (   
                    select: x => new GetTeacherVM
                    {
                        Id = x.Id,
                        FullName = x.FirstName + " " + x.LastName,
                        BirthDate = x.BirthDate.ToShortDateString(),
                        HireDate = x.HireDate.ToShortDateString(),
                        Email = x.Email,
                        CourseName = x.Course.Name,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate != null ? x.UpdatedDate.Value.ToString("d.M.yyyy HH:mm:ss") : " - ",
                        Status = x.Status == Status.Active ? "Aktif" : "Güncellenmiş"
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                    join: x => x.Include(z => z.Course)
                );

            return View(teachers);
        }

        public async Task<IActionResult> CreateTeacher()
        {
            var courses = await _courseManager.GetByDefaultsAsync<GetCourseSelectListDTO>(x => x.Status != Status.Passive);
            var coursesVM = _mapper.Map<List<GetCourseForSelectListVM>>(courses);
            var model = new CreateTeacherVM
            {
                Courses = new SelectList(coursesVM, "Id", "Name")
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeacher(CreateTeacherVM model)
        {
            var courses = await _courseManager.GetByDefaultsAsync<GetCourseSelectListDTO>(x => x.Status != Status.Passive);
            var coursesVM = _mapper.Map<List<GetCourseForSelectListVM>>(courses);
            model.Courses = new SelectList(coursesVM, "Id", "Name");

            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<CreateTeacherDTO>(model);
                var result = await _teacherManager.AddAsync(dto);

                if (result)
                {
                    TempData["Success"] = $"{model.FirstName} {model.LastName} eğitmeni sisteme kaydedilmiştir!";
                    return RedirectToAction(nameof(Index));
                }

                TempData["Error"] = "Eğitmen sisteme kaydedilemedi!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> UpdateTeacher(string id)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(id, out entityId);

            if (!guidResult)
            {
                TempData["Error"] = "Eğitmen bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var dto = await _teacherManager.GetByIdAsync<UpdateTeacherDTO>(entityId);
            if (dto != null)
            {
                var courses = await _courseManager.GetByDefaultsAsync<GetCourseSelectListDTO>(x => x.Status != Status.Passive);
                var coursesVM = _mapper.Map<List<GetCourseVM>>(courses);
                var selectedCourse = await _courseManager.GetByIdAsync<GetCourseSelectListDTO>(dto.CourseId);
                var selectedCourseVM = _mapper.Map<GetCourseVM>(selectedCourse);
                var model = _mapper.Map<UpdateTeacherVM>(dto);
                model.Courses = new SelectList(coursesVM, "Id", "Name", selectedCourseVM);
                return View(model);
            }

            TempData["Error"] = "Eğitmen bulunamadı!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTeacher(UpdateTeacherVM model)
        {
            var courses = await _courseManager.GetByDefaultsAsync<GetCourseSelectListDTO>(x => x.Status != Status.Passive);
            var coursesVM = _mapper.Map<List<GetCourseVM>>(courses);
            var selectedCourse = await _courseManager.GetByIdAsync<GetCourseSelectListDTO>((Guid)model.CourseId);
            var selectedCourseVM = _mapper.Map<GetCourseVM>(selectedCourse);
            model.Courses = new SelectList(coursesVM, "Id", "Name", selectedCourseVM);

            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<UpdateTeacherDTO>(model);
                var result = await _teacherManager.UpdateAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Eğitmen güncellendi!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Eğitmen güncellenemedi!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> DeleteTeacher(string id)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(id, out entityId);
            if (!guidResult)
            {
                TempData["Error"] = "Eğitmen bulunamadı!";
                return RedirectToAction(nameof(Index));
            }
            var dto = await _teacherManager.GetByIdAsync<DeleteTeacherDTO>(entityId);

            if (dto != null)
            {
                var result = await _teacherManager.DeleteAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Eğitmen silinmiştir!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Eğitmen silinememiştir!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Eğitmen bulunamamıştır!";
            return RedirectToAction(nameof(Index));
        }
    }
}
