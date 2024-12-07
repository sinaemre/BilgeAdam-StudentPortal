using ApplicationCore.Consts;
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
    public class CourseService : BaseRepository<Course>, ICourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context, IUserService userService) : base(context, userService)
        {
            _context = context;
        }

        public async Task<Guid> GetCourseIdByClassroomIdAsync(Guid classroomId)
        {
            var classroom = await _context.Classrooms.Include(x => x.Teacher).ThenInclude(x => x.Course).FirstOrDefaultAsync(x => x.Status != Status.Passive && x.Id == classroomId);
            var courseId = classroom.Teacher.CourseId;
            return courseId;
        }
    }
}
