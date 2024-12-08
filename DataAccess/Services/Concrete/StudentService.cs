using ApplicationCore.Consts;
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

        public async Task<int> GetSuccessStudentsPercentage()
        {
            var successStudents = await _context.Students.Where(x => x.Status != Status.Passive).ToListAsync();
            var successStudentsCount = successStudents.Count(x => x.StudentStatus == StudentStatus.Success);

            var totalStudents = await _context.Students.Where(x => x.Status != Status.Passive).ToListAsync();
            var totalStudentsCount = totalStudents.Count(x => x.StudentStatus != StudentStatus.Continue);
            if (totalStudentsCount == 0)
                return 0;
            
            var percentage = (successStudentsCount / totalStudentsCount) * 100;
            return percentage;
        }

        public async Task<int> GetStudentsCount()
        {
            var totalStudentsCount = await _context.Students.CountAsync(x => x.Status != Status.Passive);
            return totalStudentsCount;
        }
    }
}
