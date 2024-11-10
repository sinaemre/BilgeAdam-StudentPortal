using ApplicationCore.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Interface
{
    public interface ICourseService : IBaseRepository<Course>
    {
        Task<Guid> GetCourseIdByClassroomIdAsync(Guid classroomId);
    }
}
