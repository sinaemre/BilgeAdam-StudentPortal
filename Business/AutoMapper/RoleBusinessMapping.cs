using ApplicationCore.Consts;
using ApplicationCore.UserEntites.Concrete;
using AutoMapper;
using DTO.Concrete.RoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class RoleBusinessMapping : Profile
    {
        public RoleBusinessMapping()
        {
            CreateMap<AppRole, GetRoleDTO>()
                .ForMember(x => x.UpdatedDate, dest => dest.MapFrom(z => z.UpdatedDate != null ? z.UpdatedDate.ToString() : " - "))
                .ForMember(x => x.Status, dest => dest.MapFrom(z => z.Status == Status.Active ? "Aktif" : "Güncellenmiş")).ReverseMap();

            CreateMap<AppRole, CreateRoleDTO>().ReverseMap();
            CreateMap<AppRole, UpdateRoleDTO>().ReverseMap();
        }
    }
}
