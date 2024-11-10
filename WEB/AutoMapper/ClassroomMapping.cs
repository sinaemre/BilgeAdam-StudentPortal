using AutoMapper;
using DTO.Concrete.ClassroomDTO;
using WEB.Areas.Education.Models.ViewModels.Classrooms;

namespace WEB.AutoMapper
{
    public class ClassroomMapping : Profile
    {
        public ClassroomMapping()
        {
            CreateMap<GetClassroomForSelectListDTO, GetClassroomForSelectListVM>().ReverseMap();
            CreateMap<CreateClassroomDTO, CreateClassroomVM>().ReverseMap();
            CreateMap<UpdateClassroomDTO, UpdateClassroomVM>().ReverseMap();
        }
    }
}
