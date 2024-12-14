using ApplicationCore.Consts;
using ApplicationCore.Entities.Concrete;
using AutoMapper;
using Business.Manager.Interface;
using DTO.Concrete.ClassroomDTO;
using DTO.Concrete.CourseDTO;
using DTO.Concrete.TeacherDTO;
using DTO.Concrete.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB.Areas.Education.Models.ViewModels.Classrooms;
using WEB.Areas.Education.Models.ViewModels.Courses;
using WEB.Areas.Education.Models.ViewModels.Students;
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
        private readonly IUserManager _userManager;

        public ClassroomsController(IClassroomManager classroomManager, IMapper mapper, ITeacherManager teacherManager, ICourseManager courseManager, IStudentManager studentManager, IUserManager userManager)
        {
            _classroomManager = classroomManager;
            _mapper = mapper;
            _teacherManager = teacherManager;
            _courseManager = courseManager;
            _studentManager = studentManager;
            _userManager = userManager;
        }
        [Authorize(Roles = "admin, customerManager")]
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
                        StartDate = x.StartDate != null ? x.StartDate.Value.ToShortDateString() : " - ",
                        EndDate = x.EndDate != null ? x.EndDate.Value.ToShortDateString() : " - ",
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

        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> CreateClassroom()
        {
            var model = new CreateClassroomVM
            {
                Courses = await GetCourses()
            };
            return View(model);
        }

        [Authorize(Roles = "admin, customerManager")]
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

        [Authorize(Roles = "admin, customerManager")]
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

        [Authorize(Roles = "admin, customerManager")]
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

        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> DeleteClassroom(string id)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(id, out entityId);
            if (!guidResult)
            {
                TempData["Error"] = "Sınıf bulunamamıştır!";
                return RedirectToAction(nameof(Index));
            }

            var dto = await _classroomManager.GetByIdAsync<UpdateClassroomDTO>(entityId);

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

        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetClassroomsForTeacherByTeacherId()
        {
            var userDTO = await _userManager.FindUserAsync<GetUserDTO>(HttpContext.User);
            var teacher = await _teacherManager.GetByDefaultAsync<GetTeacherForSelectListDTO>(x => x.Email == userDTO.Email);
            var classrooms = await _classroomManager.GetFilteredListAsync
                (
                    select: x => new GetClassroomVM
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CourseName = x.Teacher.Course.Name,
                        Description = x.Description,
                        StartDate = x.StartDate != null ? x.StartDate.Value.ToShortDateString() : " - ",
                        EndDate = x.EndDate != null ? x.EndDate.Value.ToShortDateString() : " - ",
                        TeacherName = x.Teacher.FirstName + " " + x.Teacher.LastName,
                        ClassroomSize = x.Students.Where(x => x.Status != Status.Passive).ToList().Count,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate != null ? x.UpdatedDate.Value.ToString("d.M.yyyy HH:mm:ss") : " - ",
                        Status = x.Status == Status.Active ? "Aktif" : "Güncellenmiş"
                    },
                    where: x => x.Status != Status.Passive && x.TeacherId == teacher.Id,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                    join: x => x.Include(z => z.Teacher).ThenInclude(z => z.Course).Include(z => z.Students)
                );
            return View(classrooms);
        }

        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetStudentsByClassroomId(string classroomId)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(classroomId, out entityId);
            if (!guidResult)
            {
                TempData["Error"] = "Sınıf bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var students = await _studentManager.GetFilteredListAsync
               (
                  select: x => new StudentDetailForProjectVM
                  {
                      Id = x.Id,
                      FirstName = x.FirstName,
                      LastName = x.LastName,
                      BirthDate = x.BirthDate,
                      Email = x.Email,
                      Exam1 = x.Exam1,
                      Exam2 = x.Exam2,
                      ProjectExam = x.ProjectExam,
                      Average = x.Average,
                      StudentStatus = x.StudentStatus,
                      CourseName = x.Classroom.Teacher.Course.Name,
                      ClassroomName = x.Classroom.Name,
                      ClassroomId = x.ClassroomId,
                      ImagePath = x.ImagePath,
                      ProjectPath = x.ProjectPath,
                      ProjectName = x.ProjectName
                  },
                   where: x => x.Status != Status.Passive && x.ClassroomId == entityId,
                   orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                   join: x => x.Include(z => z.Classroom).ThenInclude(z => z.Teacher).ThenInclude(x => x.Course)
               );

            return View(students);

        }

        [Authorize(Roles = "teacher")]
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
            return new SelectList(coursesVM, "Id", "Info", selectedCourse);
        }

        private async Task<SelectList> GetCourses()
        {
            var courses = await _courseManager.GetByDefaultsAsync<GetCourseForSelectListDTO>(x => x.Status != Status.Passive);
            var coursesVM = _mapper.Map<List<GetCourseForSelectListVM>>(courses);
            return new SelectList(coursesVM, "Id", "Info");
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
