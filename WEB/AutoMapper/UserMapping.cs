using AutoMapper;
using DTO.Concrete.UserDTO;
using WEB.Areas.Admin.Models.ViewModels.Users;

namespace WEB.AutoMapper
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<GetUserForRoleVM, GetUserForRoleDTO>().ReverseMap();
        }
    }
}
