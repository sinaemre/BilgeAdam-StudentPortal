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
    public class TeacherService : BaseRepository<Teacher>, ITeacherService
    {
        public TeacherService(AppDbContext context) : base(context)
        {
        }
    }
}
