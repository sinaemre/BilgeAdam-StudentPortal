using ApplicationCore.Entities.Concrete;
using DataAccess.Context.ApplicationContext;
using DataAccess.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Concrete
{
    public class CustomerManagerService : BaseRepository<CustomerManager>, ICustomerManagerService
    {
        public CustomerManagerService(AppDbContext context) : base(context)
        {
        }
    }
}
