using ApplicationCore.Entities.Concrete;
using AutoMapper;
using Business.Manager.Interface;
using DataAccess.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager.Concrete
{
    public class CMManager : BaseManager<ICustomerManagerService, CustomerManager>, ICMManager
    {
        public CMManager(ICustomerManagerService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
