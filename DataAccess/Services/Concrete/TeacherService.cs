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
    public class TeacherService : BaseRepository<Teacher>, ITeacherService
    {
        private readonly AppDbContext _context;

        public TeacherService(AppDbContext context, IUserService userService) : base(context, userService)
        {
            _context = context;
        }

        public async Task<Guid> GetTeacherIdByClassroomIdAsync(Guid classroomId)
        {
            var classroom = await _context.Classrooms.FirstOrDefaultAsync(x => x.Id == classroomId);
            return classroom.TeacherId;
        }

        public async Task<int> GetTeachersCount()
        {
            var teachers = await _context.Teachers.Where(x => x.Status != Status.Passive).ToListAsync();
            var teachersCount = teachers.Count;
            return teachersCount;
        }
    }
}
