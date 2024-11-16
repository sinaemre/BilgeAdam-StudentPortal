using ApplicationCore.Entities.Concrete;
using AutoMapper;
using DTO.Concrete.TeacherDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class TeacherBusinessMapping : Profile
    {
        public TeacherBusinessMapping()
        {
            CreateMap<Teacher, GetTeacherForSelectListDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ReverseMap();
            CreateMap<CreateTeacherDTO, Teacher>().ReverseMap();
            CreateMap<UpdateTeacherDTO, Teacher>().ReverseMap();
            CreateMap<UpdateTeacherDTO, Teacher>().ReverseMap();
        }
    }
}
