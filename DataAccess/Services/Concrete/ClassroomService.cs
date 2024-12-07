using ApplicationCore.Entities.Concrete;
using ApplicationCore.UserEntites.Concrete;
using DataAccess.Context.ApplicationContext;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Concrete
{
    public class ClassroomService : BaseRepository<Classroom>, IClassroomService
    {
        public ClassroomService(AppDbContext context, IUserService userService) : base(context, userService)
        {
        }
    }
}
