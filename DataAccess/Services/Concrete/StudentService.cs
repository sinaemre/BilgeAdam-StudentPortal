using ApplicationCore.Entities.Concrete;
using ApplicationCore.UserEntites.Concrete;
using DataAccess.Context.ApplicationContext;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services.Concrete
{
    public class StudentService : BaseRepository<Student>, IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context, IUserService userService) : base(context, userService)
        {
            _context = context;
        }

        public double? GetTotalEarningByCourseId(Guid courseId)
        {
               return _context.Students
                             .Where(x => x.Classroom.Teacher.CourseId == courseId)
                             .ToList()
                             .Sum(x => x.RegisterPrice);
        }
    }
}
