using ApplicationCore.Consts;
using AutoMapper;
using Business.Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using WEB.Models.ViewModels.Home;

namespace WEB.ViewComponents
{
    public class EducationAwardsViewComponent : ViewComponent
    {
        private readonly IStudentManager _studentManager;
        private readonly ITeacherManager _teacherManager;

        public EducationAwardsViewComponent(IStudentManager studentManager, ITeacherManager teacherManager)
        {
            _studentManager = studentManager;
            _teacherManager = teacherManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var successStudentsPercentage = await _studentManager.GetSuccessStudentsPercentage();
            var studentsCount = await _studentManager.GetStudentsCount();
            var teachersCount = await _teacherManager.GetTeachersCount();

            var model = new GetEducationAwardsVM
            {
                SuccessStudentsPercentage = successStudentsPercentage,
                NewStudentsCount = studentsCount,
                TeachersCount = teachersCount
            };

            return View(model);
        }
    }
}
