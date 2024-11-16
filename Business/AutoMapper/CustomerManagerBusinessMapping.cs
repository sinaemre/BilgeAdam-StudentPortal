using ApplicationCore.Entities.Concrete;
using AutoMapper;
using DTO.Concrete.CustomerManagerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class CustomerManagerBusinessMapping : Profile
    {
        public CustomerManagerBusinessMapping()
        {
            CreateMap<CreateCustomerManagerDTO, CustomerManager>().ReverseMap();
            CreateMap<UpdateCustomerManagerDTO, CustomerManager>().ReverseMap();
        }
    }
}
