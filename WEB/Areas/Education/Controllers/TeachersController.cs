using ApplicationCore.Consts;
using ApplicationCore.Entities.Concrete;
using AutoMapper;
using Business.Manager.Interface;
using DTO.Concrete.TeacherDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
                        UpdatedDate = x.UpdatedDate != null ? x.UpdatedDate.ToString() : " - ",
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
            var courses = await _courseManager.GetByDefaultsAsync<Course>(x => x.Status != Status.Passive);
            var model = new CreateTeacherVM
            {
                Courses = new SelectList(courses, "Id", "Name")
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeacher(CreateTeacherVM model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<CreateTeacherDTO>(model);
                var result = await _teacherManager.AddAsync(dto);

                if (result)
                {
                    TempData["Success"] = $"{model.FirstName} {model.LastName} eğitmeni sisteme kaydedilmiştir!";
                    return RedirectToAction("Index");
                }

                TempData["Error"] = "Eğitmen sisteme kaydedilemedi!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }
    }
}
