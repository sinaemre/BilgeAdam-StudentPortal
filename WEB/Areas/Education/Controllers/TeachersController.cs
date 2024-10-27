using ApplicationCore.Consts;
using AutoMapper;
using Business.Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Areas.Education.Models.ViewModels.Teachers;

namespace WEB.Areas.Education.Controllers
{
    [Area("Education")]
    public class TeachersController : Controller
    {
        private readonly ITeacherManager _teacherManager;
        private readonly IMapper _mapper;

        public TeachersController(ITeacherManager teacherManager, IMapper mapper)
        {
            _teacherManager = teacherManager;
            _mapper = mapper;
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
    }
}
