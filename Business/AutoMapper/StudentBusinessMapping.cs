using ApplicationCore.Entities.Concrete;
using AutoMapper;
using DTO.Concrete.StudentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class StudentBusinessMapping : Profile
    {
        public StudentBusinessMapping()
        {
            CreateMap<CreateStudentDTO, Student>().ReverseMap();
            CreateMap<UpdateStudentDTO, Student>().ReverseMap();
            CreateMap<StudentDetailForProjectDTO, Student>().ReverseMap();
        }
    }
}
