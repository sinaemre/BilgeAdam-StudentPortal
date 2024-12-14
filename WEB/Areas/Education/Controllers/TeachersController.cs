using ApplicationCore.Consts;
using ApplicationCore.UserEntites.Concrete;
using AutoMapper;
using Business.Manager.Interface;
using DTO.Concrete.CourseDTO;
using DTO.Concrete.TeacherDTO;
using DTO.Concrete.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB.Areas.Education.Models.ViewModels.Courses;
using WEB.Areas.Education.Models.ViewModels.Teachers;

namespace WEB.Areas.Education.Controllers
{
    [Area("Education")]
    [Authorize(Roles = "admin, customerManager")]
    public class TeachersController : Controller
    {
        private readonly ITeacherManager _teacherManager;
        private readonly IMapper _mapper;
        private readonly ICourseManager _courseManager;
        private readonly IUserManager _userManager;
        private readonly IEmailSender _emailSender;

        public TeachersController(ITeacherManager teacherManager, IMapper mapper, ICourseManager courseManager, IUserManager userManager, IEmailSender emailSender)
        {
            _teacherManager = teacherManager;
            _mapper = mapper;
            _courseManager = courseManager;
            _userManager = userManager;
            _emailSender = emailSender;
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
            var model = new CreateTeacherVM
            {
                Courses = await GetCourses()
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeacher(CreateTeacherVM model)
        {
            model.Courses = await GetCourses();

            if (ModelState.IsValid)
            {
                var appUserDto = _mapper.Map<CreateUserDTO>(model);
                var resultApp = await _userManager.CreateUserAsync(appUserDto);
                if (resultApp)
                {
                    var resultRole = await _userManager.AddUserToRoleAsync(appUserDto.Email, "teacher");
                    if (resultRole)
                    {
                        var appUser = await _userManager.FindUserByEmailAsync(appUserDto.Email);
                        var dto = _mapper.Map<CreateTeacherDTO>(model);
                        var result = await _teacherManager.AddAsync(dto);
                        if (result)
                        {
                            var token = await _userManager.GenerateTokenForResetPassword(appUser.Id);
                            var callBackURL = Url.Action("CreatePassword", "Account", 
                                new { area=string.Empty, token, email = appUser.Email }, Request.Scheme);

                            var message = $" <p>Kullanıcı Adınız: {appUser.UserName}</p><br>    <p>Şifreniz: {appUser.FirstPassword}</p><br><p>Lütfen şifrenizi sıfırlamak için <a href=\"{callBackURL}\">buraya tıklayınız!</a> </p>";

                            await _emailSender.SendEmailAsync(appUser.Email, "Yeni Kayıt Oluşturma Şifre Değişikliği", message);

                            TempData["Success"] = $"{model.FirstName} {model.LastName} eğitmeni sisteme kaydedilmiştir!";
                            return RedirectToAction(nameof(Index));
                        }
                        TempData["Error"] = "Eğitmen sisteme kaydedilemedi!";
                        return View(model);
                    }
                    TempData["Error"] = "Eğitmen role kaydedilemedi!";
                    return View(model);
                    
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
                var model = _mapper.Map<UpdateTeacherVM>(dto);
                model.Courses = await GetCourses(dto.CourseId);
                return View(model);
            }

            TempData["Error"] = "Eğitmen bulunamadı!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTeacher(UpdateTeacherVM model)
        {
            model.Courses = await GetCourses(model.CourseId);

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
            var dto = await _teacherManager.GetByIdAsync<UpdateTeacherDTO>(entityId);

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

        public async Task<IActionResult> GetTeachersByCourseId(string courseId)
        {
            Guid entityId;
            var guidResult = Guid.TryParse(courseId, out entityId);
            if (!guidResult)
            {
                TempData["Error"] = "Eğitmen bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var teachersDTO = await _teacherManager.GetByDefaultsAsync<GetTeacherForSelectListDTO>(x => x.CourseId == entityId);
            var teacherVM = _mapper.Map<List<GetTeacherForSelectListVM>>(teachersDTO);
            return Json(teacherVM);
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
    }
}
