using ApplicationCore.Consts;
using ApplicationCore.Entities.Concrete;
using AutoMapper;
using Business.Manager.Interface;
using DTO.Concrete.ClassroomDTO;
using DTO.Concrete.CourseDTO;
using DTO.Concrete.TeacherDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB.Areas.Education.Models.ViewModels.Classrooms;
using WEB.Areas.Education.Models.ViewModels.Courses;
using WEB.Areas.Education.Models.ViewModels.Teachers;

namespace WEB.Areas.Education.Controllers
{
    [Area("Education")]
    public class ClassroomsController : Controller
    {
        private readonly IClassroomManager _classroomManager;
        private readonly IMapper _mapper;
        private readonly ITeacherManager _teacherManager;
        private readonly ICourseManager _courseManager;
        private readonly IStudentManager _studentManager;

        public ClassroomsController(IClassroomManager classroomManager, IMapper mapper, ITeacherManager teacherManager, ICourseManager courseManager, IStudentManager studentManager)
        {
            _classroomManager = classroomManager;
            _mapper = mapper;
            _teacherManager = teacherManager;
            _courseManager = courseManager;
            _studentManager = studentManager;
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
                Courses = await GetCourses()
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClassroom(CreateClassroomVM model)
        {
            model.Courses = await GetCourses();

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

        
        public async Task<IActionResult> UpdateClassroom(string id)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(id, out entityId);
            if (!guidResult)
            {
                TempData["Error"] = "Sınıf bulunamamıştır!";
                return RedirectToAction(nameof(Index));
            }
            var classroomDto = await _classroomManager.GetByIdAsync<UpdateClassroomDTO>(entityId);
            if (classroomDto != null)
            {
                var model = _mapper.Map<UpdateClassroomVM>(classroomDto);
                var courseId = await _courseManager.GetCourseIdByClassroomIdAsync(entityId);
                model.Courses = await GetCourses(courseId);
                model.Teachers = await GetTeachers(model.TeacherId);
                model.CourseId = courseId;
                return View(model);
            }
            TempData["Error"] = "Sınıf bulunamamıştır!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateClassroom(UpdateClassroomVM model)
        {
            var courseId = await _courseManager.GetCourseIdByClassroomIdAsync(model.Id);
            model.Courses = await GetCourses(courseId);
            model.Teachers = await GetTeachers(model.TeacherId);
            model.CourseId = courseId;

            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<UpdateClassroomDTO>(model);
                var result = await _classroomManager.UpdateAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Sınıf güncellenmiştir!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Sınıf güncellenememiştir!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);

        }


        public async Task<IActionResult> DeleteClassroom(string id)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(id, out entityId);
            if (!guidResult)
            {
                TempData["Error"] = "Sınıf bulunamamıştır!";
                return RedirectToAction(nameof(Index));
            }

            var dto = await _classroomManager.GetByIdAsync<DeleteClassroomDTO>(entityId);
            
            if (dto != null)
            {
                var checkStudents = await _studentManager.AnyAsync(x => x.Status != Status.Passive && x.ClassroomId == entityId);
                if (checkStudents)
                {
                    TempData["Error"] = "Bu sınıfa kayıtlı öğrenciler vardır. Silinemez!";
                    return RedirectToAction(nameof(Index));
                }

                var result = await _classroomManager.DeleteAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Sınıf silinmiştir!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Sınıf silinememiştir!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Sınıf bulunamamıştır!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetClassroomsByTeacherId(string teacherId)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(teacherId, out entityId);
            if (!guidResult)
            {
                TempData["Error"] = "Sınıf bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var classrooms = await _classroomManager.GetByDefaultsAsync<GetClassroomForSelectListDTO>(x => x.TeacherId == entityId);
            var classroomsVM = _mapper.Map<List<GetClassroomForSelectListVM>>(classrooms);
            return Json(classroomsVM);
        }

        private async Task<SelectList> GetCourses(Guid? courseId)
        {
            var courses = await _courseManager.GetByDefaultsAsync<GetCourseForSelectListDTO>(x => x.Status != Status.Passive);
            var coursesVM = _mapper.Map<List<GetCourseForSelectListVM>>(courses);
            var selectedCourse = await _courseManager.GetByIdAsync<GetCourseForSelectListDTO>((Guid)courseId);
            return new SelectList(coursesVM, "Id", "Name", selectedCourse);
        }
        private async Task<SelectList> GetCourses()
        {
            var courses = await _courseManager.GetByDefaultsAsync<GetCourseForSelectListDTO>(x => x.Status != Status.Passive);
            var coursesVM = _mapper.Map<List<GetCourseForSelectListVM>>(courses);
            return new SelectList(coursesVM, "Id", "Name");
        }

        private async Task<SelectList> GetTeachers(Guid? teacherId)
        {
            var teachers = await _teacherManager.GetByDefaultsAsync<GetTeacherForSelectListDTO>(x => x.Status != Status.Passive);
            var teachersVM = _mapper.Map<List<GetTeacherForSelectListVM>>(teachers);
            var selectedTeacher = await _teacherManager.GetByIdAsync<GetTeacherForSelectListDTO>((Guid)teacherId);
            var selectedTeacherVM = _mapper.Map<GetTeacherForSelectListVM>(selectedTeacher);
            return new SelectList(teachersVM, "Id", "FullName", selectedTeacherVM);
        }
    }
}
