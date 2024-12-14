using ApplicationCore.Consts;
using ApplicationCore.Entities.Concrete;
using AutoMapper;
using Business.Manager.Concrete;
using Business.Manager.Interface;
using DTO.Concrete.ClassroomDTO;
using DTO.Concrete.CourseDTO;
using DTO.Concrete.StudentDTO;
using DTO.Concrete.TeacherDTO;
using DTO.Concrete.UserDTO;
using Humanizer;
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
    public class StudentsController : Controller
    {
        private readonly IStudentManager _studentManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICourseManager _courseManager;
        private readonly IClassroomManager _classroomManager;
        private readonly ITeacherManager _teacherManager;
        private readonly IUserManager _userManager;

        public StudentsController(IStudentManager studentManager, IMapper mapper, IWebHostEnvironment webHostEnvironment, ICourseManager courseManager, IClassroomManager classroomManager, ITeacherManager teacherManager, IUserManager userManager)
        {
            _studentManager = studentManager;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _courseManager = courseManager;
            _classroomManager = classroomManager;
            _teacherManager = teacherManager;
            _userManager = userManager;
        }

        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> Index()
        {
            var students = await _studentManager.GetFilteredListAsync
                (
                    select: x => new GetStudentVM
                    {
                        Id = x.Id,
                        FullName = x.FirstName + " " + x.LastName,
                        BirthDate = x.BirthDate.ToShortDateString(),
                        Email = x.Email,
                        RegisterPrice = x.RegisterPrice != null ? x.RegisterPrice.Value.ToString("C2") : " - ",
                        CourseName = x.Classroom.Teacher.Course.Name,
                        ClassroomName = x.Classroom.Name,
                        TeacherName = x.Classroom.Teacher.FirstName + " " + x.Classroom.Teacher.LastName,
                        Average = x.Average,
                        Status = x.Status == Status.Active ? "Aktif" : "Güncellenmiş",
                        StudentStatus = x.StudentStatus == StudentStatus.Success ?
                                                      "Mezun" : x.StudentStatus == StudentStatus.Continue ?
                                                      "Devam Ediyor" : "Kaldı",
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate != null ? x.UpdatedDate.Value.ToString("d.M.yyyy HH:mm:ss") : " - "
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                    join: x => x.Include(z => z.Classroom).ThenInclude(z => z.Teacher).ThenInclude(x => x.Course)
                );

            return View(students);
        }

        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> CreateStudent()
        {
            var model = new CreateStudentVM
            {
                Courses = await GetCourses()
            };

            return View(model);
        }

        [Authorize(Roles = "admin, customerManager")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent(CreateStudentVM model)
        {
            model.Courses = await GetCourses();

            if (ModelState.IsValid)
            {
                var appUserDto = _mapper.Map<CreateUserDTO>(model);
                var resultApp = await _userManager.CreateUserAsync(appUserDto);
                if (resultApp)
                {
                    var resultRole = await _userManager.AddUserToRoleAsync(appUserDto.Email, "student");
                    if (resultRole)
                    {
                        string imageName = "noimage.png";

                        if (model.Image != null)
                        {
                            var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                            imageName = $"{Guid.NewGuid()}_{model.FirstName}_{model.LastName}_{model.Image.FileName}";
                            var filePath = Path.Combine(uploadDir, imageName);
                            FileStream fileStream = new FileStream(filePath, FileMode.Create);
                            await model.Image.CopyToAsync(fileStream);
                            fileStream.Close();
                        }

                        var dto = _mapper.Map<CreateStudentDTO>(model);
                        dto.ImagePath = imageName;
                        var result = await _studentManager.AddAsync(dto);
                        if (result)
                        {
                            TempData["Success"] = $"{dto.FirstName} {dto.LastName} öğrencisi sisteme kayıt edilmiştir!";
                            return RedirectToAction(nameof(Index));
                        }
                        TempData["Error"] = "Öğrenci sisteme kaydedilemedi!";
                        return View(model);
                    }
                    TempData["Error"] = "Öğrenci role kaydedilemedi!";
                    return View(model);
                }

                TempData["Error"] = "Öğrenci sisteme kaydedilemedi!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> UpdateStudent(string id)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(id, out entityId);
            if (!guidResult)
            {
                TempData["Error"] = "Öğrenci bulunamamıştır!";
                return RedirectToAction(nameof(Index));
            }

            var dto = await _studentManager.GetByIdAsync<UpdateStudentDTO>(entityId);
            if (dto != null)
            {
                var model = _mapper.Map<UpdateStudentVM>(dto);

                model.Courses = await GetCourses(model.ClassroomId);
                model.Classrooms = await GetClassrooms(model.ClassroomId);
                model.Teachers = await GetTeachers(model.ClassroomId);
                model.TeacherId = await _teacherManager.GetTeacherIdByClassroomIdAsync((Guid)model.ClassroomId);
                model.CourseId = await _courseManager.GetCourseIdByClassroomIdAsync((Guid)model.ClassroomId);
                return View(model);
            }

            TempData["Error"] = "Öğrenci bulunamamıştır!";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin, customerManager")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStudent(UpdateStudentVM model)
        {
            model.Courses = await GetCourses(model.ClassroomId);
            model.Classrooms = await GetClassrooms(model.ClassroomId);
            model.Teachers = await GetTeachers(model.ClassroomId);

            if (ModelState.IsValid)
            {
                var imageName = model.ImagePath;

                if (model.Image != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "img");

                    //Eski Resmi Silme Bölümü
                    if (model.ImagePath != null && !string.Equals(model.ImagePath, "noimage.png"))
                    {
                        string oldPath = Path.Combine(uploadDir, model.ImagePath);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    imageName = $"{Guid.NewGuid()}_{model.FirstName}_{model.LastName}_{model.Image.FileName}";
                    var filePath = Path.Combine(uploadDir, imageName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    await model.Image.CopyToAsync(fileStream);
                    fileStream.Close();
                }

                var dto = _mapper.Map<UpdateStudentDTO>(model);
                dto.ImagePath = imageName;

                var result = await _studentManager.UpdateAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Öğrenci güncellenmiştir!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Öğrenci güncellenememiştir!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(id, out entityId);
            if (!guidResult)
            {
                TempData["Error"] = "Öğrenci bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var dto = await _studentManager.GetByIdAsync<UpdateStudentDTO>(entityId);

            if (dto != null)
            {
                var result = await _studentManager.DeleteAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Öğrenci silinmiştir!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Öğrenci silinememiştir!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Öğrenci bulunamadı!";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin, customerManager, student, teacher")]
        public async Task<IActionResult> StudentDetail(string studentId = null)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                string studentEmail = "";
                if (HttpContext.User.IsInRole("student"))
                {
                    var studentUser = await _userManager.FindUserAsync<GetUserDTO>(HttpContext.User);
                    studentEmail = studentUser.Email;
                }
                else if (HttpContext.User.IsInRole("teacher") && studentId != null)
                {
                    Guid entityId;
                    var guidResult = Guid.TryParse(studentId, out entityId);
                    if (!guidResult)
                    {
                        TempData["Error"] = "Öğrenci bulunamadı!";
                        return RedirectToAction(nameof(Index));
                    }
                    var student = await _studentManager.GetByIdAsync<StudentDetailForProjectDTO>((Guid)entityId);
                    studentEmail = student.Email;
                }

                var studentDtos = await _studentManager.GetFilteredListAsync
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
                            where: x => x.Email == studentEmail,
                            join: x => x.Include(z => z.Classroom).ThenInclude(z => z.Teacher).ThenInclude(z => z.Course)
                        );
                var studentDto = studentDtos.FirstOrDefault();

                if (studentDto != null)
                {
                    var model = _mapper.Map<StudentDetailForProjectVM>(studentDto);
                    return View(model);
                }
                TempData["Error"] = "Öğrenci bulunamadı!";
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [Authorize(Roles = "admin, customerManager, student")]
        public async Task<IActionResult> SendProject(StudentDetailForProjectVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Project != null)
                {
                    var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "projects");
                    string fileName = $"{Guid.NewGuid()}_{model.FirstName}_{model.LastName}_{model.Project.FileName}";
                    string filePath = Path.Combine(uploadDir, fileName);

                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    await model.Project.CopyToAsync(fileStream);
                    fileStream.Close();

                    var dto = _mapper.Map<StudentDetailForProjectDTO>(model);
                    dto.ProjectPath = fileName;
                    var result = await _studentManager.UpdateAsync(dto);

                    if (result)
                    {
                        TempData["Success"] = "Proje yüklenmiştir!";
                        return RedirectToAction(nameof(StudentDetail));
                    }
                    TempData["Error"] = "Proje yüklenememiştir!";
                    return RedirectToAction(nameof(StudentDetail));
                }
                return RedirectToAction(nameof(StudentDetail));
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return RedirectToAction(nameof(StudentDetail));
        }

        [Authorize(Roles = "admin, customerManager, teacher")]
        public async Task<IActionResult> UpdateStudentExams(StudentDetailForProjectVM model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<StudentDetailForProjectDTO>(model);
                var result = await _studentManager.UpdateAsync(dto);

                if (result)
                {
                    TempData["Success"] = "Notlar girilmiştir!";
                    return RedirectToAction(nameof(StudentDetail), new { studentId = model.Id.ToString() });
                }
                TempData["Error"] = "Proje yüklenememiştir!";
                return RedirectToAction(nameof(StudentDetail));
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return RedirectToAction(nameof(StudentDetail));
        }

        [Authorize(Roles = "admin, customerManager, student, teacher")]
        public FileResult Download(string filePath)
        {
            string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "projects/");
            byte[] fileBytes = System.IO.File.ReadAllBytes(uploadDir + filePath);

            //System.Net.Mime.MediaTypeNames.Application.Octet => Gelen dosyanın hengi türde olduğunu bilmediğimiz zaman kullanılır.
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);
        }

        private async Task<SelectList> GetCourses(Guid? classroomId)
        {
            var courseId = await _courseManager.GetCourseIdByClassroomIdAsync((Guid)classroomId);
            var courses = await _courseManager.GetByDefaultsAsync<GetCourseForSelectListDTO>(x => x.Status != Status.Passive);
            var coursesVM = _mapper.Map<List<GetCourseForSelectListVM>>(courses);
            var selectedCourse = await _courseManager.GetByIdAsync<GetCourseForSelectListDTO>(courseId);
            return new SelectList(coursesVM, "Id", "Info", selectedCourse);
        }
        private async Task<SelectList> GetCourses()
        {
            var courses = await _courseManager.GetByDefaultsAsync<GetCourseForSelectListDTO>(x => x.Status != Status.Passive);
            var coursesVM = _mapper.Map<List<GetCourseForSelectListVM>>(courses);
            return new SelectList(coursesVM, "Id", "Info");
        }
        private async Task<SelectList> GetClassrooms(Guid? classroomId)
        {
            var classrooms = await _classroomManager.GetByDefaultsAsync<GetClassroomForSelectListDTO>(x => x.Status != Status.Passive);
            var classroomsVM = _mapper.Map<List<GetClassroomForSelectListVM>>(classrooms);
            var selectedClassroom = await _classroomManager.GetByIdAsync<GetClassroomForSelectListDTO>((Guid)classroomId);
            return new SelectList(classroomsVM, "Id", "Name", selectedClassroom);
        }
        private async Task<SelectList> GetTeachers(Guid? classroomId)
        {
            var teacherId = await _teacherManager.GetTeacherIdByClassroomIdAsync((Guid)classroomId);
            var teachers = await _teacherManager.GetByDefaultsAsync<GetTeacherForSelectListDTO>(x => x.Status != Status.Passive);
            var teachersVM = _mapper.Map<List<GetTeacherForSelectListVM>>(teachers);
            var selectedTeacher = await _teacherManager.GetByIdAsync<GetTeacherForSelectListDTO>(teacherId);
            var selectedTeacherVM = _mapper.Map<GetTeacherForSelectListVM>(selectedTeacher);
            return new SelectList(teachersVM, "Id", "FullName", selectedTeacherVM);
        }
    }
}
