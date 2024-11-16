using AutoMapper;
using DTO.Concrete.CustomerManagerDTO;
using WEB.Areas.Admin.Models.ViewModels.CustomerManagers;

namespace WEB.AutoMapper
{
    public class CustomerManagerMapping : Profile
    {
        public CustomerManagerMapping()
        {
            CreateMap<CreateCustomerManagerVM, CreateCustomerManagerDTO>().ReverseMap();
            CreateMap<UpdateCustomerManagerVM, UpdateCustomerManagerDTO>().ReverseMap();
        }
    }
}
