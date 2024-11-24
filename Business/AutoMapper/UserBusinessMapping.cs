using ApplicationCore.UserEntites.Concrete;
using AutoMapper;
using DTO.Concrete.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class UserBusinessMapping : Profile
    {
        public UserBusinessMapping()
        {
            CreateMap<AppUser, GetUserForRoleDTO>().ReverseMap();
            CreateMap<AppUser, GetUserDTO>().ReverseMap();
            CreateMap<AppUser, CreateUserDTO>().ReverseMap();
        }
    }
}
