using AutoMapper;
using DTO.Concrete.AccountDTO;
using WEB.Models.ViewModels.Account;

namespace WEB.AutoMapper;

public class AccountMapping : Profile
{
    public AccountMapping()
    {
        CreateMap<LoginVM, LoginDTO>().ReverseMap();
        CreateMap<EditUserVM, EditUserDTO>().ReverseMap();
        CreateMap<ChangePasswordVM, ChangePasswordDTO>().ReverseMap();
        CreateMap<CreatePasswordVM, CreatePasswordDTO>().ReverseMap();
        CreateMap<ResetPasswordVM, ResetPasswordDTO>().ReverseMap();
    }
}