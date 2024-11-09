using AutoMapper;
using DTO.Concrete.TeacherDTO;
using WEB.Areas.Education.Models.ViewModels.Teachers;

namespace WEB.AutoMapper
{
    public class TeacherMapping : Profile
    {
        public TeacherMapping()
        {
            CreateMap<CreateTeacherDTO, CreateTeacherVM>().ReverseMap();
            CreateMap<UpdateTeacherDTO, UpdateTeacherVM>().ReverseMap();
            CreateMap<GetTeacherForSelectListDTO, GetTeacherForSelectListVM>().ReverseMap();
        }
    }
}
