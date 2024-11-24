using AutoMapper;
using DTO.Concrete.UserDTO;
using WEB.Areas.Admin.Models.ViewModels.CustomerManagers;
using WEB.Areas.Admin.Models.ViewModels.Users;
using WEB.Areas.Education.Models.ViewModels.Students;
using WEB.Areas.Education.Models.ViewModels.Teachers;

namespace WEB.AutoMapper
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<GetUserForRoleVM, GetUserForRoleDTO>().ReverseMap();
            CreateMap<CreateUserDTO, CreateTeacherVM>().ReverseMap();
            CreateMap<CreateUserDTO, CreateStudentVM>().ReverseMap();
            CreateMap<CreateUserDTO, CreateCustomerManagerVM>().ReverseMap();
        }
    }
}
