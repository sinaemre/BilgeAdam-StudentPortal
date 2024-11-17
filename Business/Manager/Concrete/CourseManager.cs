using ApplicationCore.Consts;
using ApplicationCore.Entities.Concrete;
using AutoMapper;
using Business.Manager.Interface;
using DataAccess.Services.Interface;
using DTO.Concrete.CourseDTO;
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
        private readonly IStudentService _studentService;

        public CourseManager(ICourseService service, IMapper mapper, IStudentService studentService) : base(service, mapper)
        {
            _service = service;
            _studentService = studentService;
        }

        public async Task<Guid> GetCourseIdByClassroomIdAsync(Guid classroomId)
        {
            var courseId = await _service.GetCourseIdByClassroomIdAsync(classroomId);
            return courseId;
        }

        public async Task<List<GetCoursesForEarningsDTO>> GetCoursesForEarnings()
        {
            var courses = await _service.GetFilteredListAsync
                (
                    select: x => new GetCoursesForEarningsDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate)
                );
            foreach (var course in courses)
            {
                course.TotalEarning = _studentService.GetTotalEarningByCourseId(course.Id);
            }
            return courses;
        }
    }
}
