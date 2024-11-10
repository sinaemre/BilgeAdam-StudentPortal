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
    public class CourseManager : BaseManager<ICourseService, Course>, ICourseManager
    {
        private readonly ICourseService _service;

        public CourseManager(ICourseService service, IMapper mapper) : base(service, mapper)
        {
            _service = service;
        }

        public async Task<Guid> GetCourseIdByClassroomIdAsync(Guid classroomId)
        {
            var courseId = await _service.GetCourseIdByClassroomIdAsync(classroomId);
            return courseId;
        }
    }
}
