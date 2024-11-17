using ApplicationCore.Entities.Concrete;
using AutoMapper;
using DTO.Concrete.CourseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class CourseBusinessMapping : Profile
    {
        public CourseBusinessMapping()
        {
            CreateMap<Course, GetCourseForSelectListDTO>().ReverseMap();
            CreateMap<Course, CreateCourseDTO>().ReverseMap();
            CreateMap<Course, UpdateCourseDTO>().ReverseMap();
            CreateMap<Course, UpdateCourseDTO>().ReverseMap();
            CreateMap<Course, GetCourseDTO>().ReverseMap();
        }
    }
}
