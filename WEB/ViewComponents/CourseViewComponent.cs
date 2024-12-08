using ApplicationCore.Consts;
using AutoMapper;
using Business.Manager.Interface;
using DTO.Concrete.CourseDTO;
using Microsoft.AspNetCore.Mvc;
using WEB.Areas.Education.Models.ViewModels.Courses;

namespace WEB.ViewComponents
{
    public class CourseViewComponent : ViewComponent
    {
        private readonly ICourseManager _courseManager;
        private readonly IMapper _mapper;

        public CourseViewComponent(ICourseManager courseManager, IMapper mapper)
        {
            _courseManager = courseManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var courses = await _courseManager.GetByDefaultsAsync<GetCourseDTO>(x => x.Status != Status.Passive);
            var model = _mapper.Map<List<GetCourseVM>>(courses);

            return View(model);
        }
    }
}
