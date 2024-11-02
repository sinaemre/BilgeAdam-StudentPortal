using AutoMapper;
using DTO.Concrete.CourseDTO;
using WEB.Areas.Education.Models.ViewModels.Courses;

namespace WEB.AutoMapper
{
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<GetCourseVM, GetCourseDTO>().ReverseMap();
        }
    }
}
