using AutoMapper;
using DTO.Concrete.RoleDTO;
using WEB.Areas.Admin.Models.ViewModels.Roles;

namespace WEB.AutoMapper
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<GetRoleVM, GetRoleDTO>().ReverseMap();
            CreateMap<CreateRoleVM, CreateRoleDTO>().ReverseMap();
            CreateMap<UpdateRoleVM, UpdateRoleDTO>().ReverseMap();

        }
    }
}
