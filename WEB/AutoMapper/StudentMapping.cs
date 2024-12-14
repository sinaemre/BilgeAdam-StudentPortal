using AutoMapper;
using DTO.Concrete.StudentDTO;
using WEB.Areas.Education.Models.ViewModels.Students;

namespace WEB.AutoMapper
{
    public class StudentMapping : Profile
    {
        public StudentMapping()
        {
            CreateMap<CreateStudentVM, CreateStudentDTO>().ReverseMap();
            CreateMap<UpdateStudentVM, UpdateStudentDTO>().ReverseMap();
            CreateMap<StudentDetailForProjectVM, StudentDetailForProjectDTO>().ReverseMap();
        }
    }
}
