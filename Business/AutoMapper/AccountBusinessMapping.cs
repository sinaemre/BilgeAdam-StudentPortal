using ApplicationCore.UserEntites.Concrete;
using AutoMapper;
using DTO.Concrete.AccountDTO;
using DTO.Concrete.UserDTO;

namespace Business.AutoMapper;

public class AccountBusinessMapping : Profile
{
    public AccountBusinessMapping()
    {
        CreateMap<EditUserDTO, AppUser>().ReverseMap();
        CreateMap<GetUserDTO, AppUser>().ReverseMap();
    }
}