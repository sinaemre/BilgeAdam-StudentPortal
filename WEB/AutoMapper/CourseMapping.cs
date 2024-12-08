using AutoMapper;
using DTO.Concrete.CourseDTO;
using WEB.Areas.Education.Models.ViewModels.Courses;

namespace WEB.AutoMapper
{
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<GetCourseForSelectListDTO, GetCourseForSelectListVM>()
                .ForMember(dest => dest.Info, src => src.MapFrom(x => x.Name + " - " + x.TotalHour + " Saat"))
                .ReverseMap();
            CreateMap<GetCourseVM, GetCourseDTO>().ReverseMap();
            CreateMap<CreateCourseVM, CreateCourseDTO>().ReverseMap();
            CreateMap<UpdateCourseVM, UpdateCourseDTO>().ReverseMap();
        }
    }
}
