using ApplicationCore.Consts;
using AutoMapper;
using Business.Manager.Interface;
using DTO.Concrete.ClassroomDTO;
using DTO.Concrete.TeacherDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB.Areas.Education.Models.ViewModels.Classrooms;
using WEB.Areas.Education.Models.ViewModels.Teachers;

namespace WEB.Areas.Education.Controllers
{
    [Area("Education")]
    public class ClassroomsController : Controller
    {
        private readonly IClassroomManager _classroomManager;
        private readonly IMapper _mapper;
        private readonly ITeacherManager _teacherManager;

        public ClassroomsController(IClassroomManager classroomManager, IMapper mapper, ITeacherManager teacherManager)
        {
            _classroomManager = classroomManager;
            _mapper = mapper;
            _teacherManager = teacherManager;
        }

        public async Task<IActionResult> Index()
        {
            var classrooms = await _classroomManager.GetFilteredListAsync
                (
                    select: x => new GetClassroomVM
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CourseName = x.Teacher.Course.Name,
                        Description = x.Description,
                        TeacherName = x.Teacher.FirstName + " " + x.Teacher.LastName,
                        ClassroomSize = x.Students.Where(x => x.Status != Status.Passive).ToList().Count,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate != null ? x.UpdatedDate.Value.ToString("d.M.yyyy HH:mm:ss") : " - ",
                        Status = x.Status == Status.Active ? "Aktif" : "Güncellenmiş"
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                    join: x => x.Include(z => z.Teacher).ThenInclude(z => z.Course).Include(z => z.Students)
                );
            return View(classrooms);
        }

        public async Task<IActionResult> CreateClassroom()
        {
            var model = new CreateClassroomVM
            {
                Teachers = await GetTeachers()
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClassroom(CreateClassroomVM model)
        {
            model.Teachers = await GetTeachers();

            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<CreateClassroomDTO>(model);
                var result = await _classroomManager.AddAsync(dto);

                if (result)
                {
                    TempData["Success"] = $"{dto.Name} sınıfı sisteme kaydedilmiştir!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Sınıf sisteme kaydedilememiştir!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

























        private async Task<SelectList> GetTeachers(Guid? teacherId)
        {
            var teachers = await _teacherManager.GetByDefaultsAsync<GetTeacherForSelectListDTO>(x => x.Status != Status.Passive);
            var teachersVM = _mapper.Map<List<GetTeacherForSelectListVM>>(teachers);
            var selectedTeacher = await _teacherManager.GetByIdAsync<GetTeacherForSelectListDTO>((Guid)teacherId);
            return new SelectList(teachersVM, "Id", "FullName", selectedTeacher);
        }
        private async Task<SelectList> GetTeachers()
        {
            var teachers = await _teacherManager.GetByDefaultsAsync<GetTeacherForSelectListDTO>(x => x.Status != Status.Passive);
            var teachersVM = _mapper.Map<List<GetTeacherForSelectListVM>>(teachers);
            return new SelectList(teachersVM, "Id", "FullName");
        }
    }
}
