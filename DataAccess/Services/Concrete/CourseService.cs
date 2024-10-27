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
    public class CourseService : BaseRepository<Course>, ICourseService
    {
        public CourseService(AppDbContext context) : base(context)
        {
        }
    }
}
