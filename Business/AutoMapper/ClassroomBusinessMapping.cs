using ApplicationCore.Entities.Concrete;
using AutoMapper;
using DTO.Concrete.ClassroomDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class ClassroomBusinessMapping : Profile
    {
        public ClassroomBusinessMapping()
        {
            CreateMap<GetClassroomForSelectListDTO, Classroom>().ReverseMap();
            CreateMap<CreateClassroomDTO, Classroom>().ReverseMap();
            CreateMap<UpdateClassroomDTO, Classroom>().ReverseMap();
            CreateMap<UpdateClassroomDTO, Classroom>().ReverseMap();

        }
    }
}
