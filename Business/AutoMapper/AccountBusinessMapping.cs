using ApplicationCore.UserEntites.Concrete;
using AutoMapper;
using DTO.Concrete.AccountDTO;

namespace Business.AutoMapper;

public class AccountBusinessMapping : Profile
{
  public AccountBusinessMapping()
  {
    CreateMap<EditUserDTO, AppUser>().ReverseMap();
  }
}