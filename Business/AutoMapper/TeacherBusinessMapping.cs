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
            CreateMap<Teacher, CreateTeacherDTO>().ReverseMap();
        }
    }
}
