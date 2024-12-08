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
    public class TeacherManager : BaseManager<ITeacherService, Teacher>, ITeacherManager
    {
        private readonly ITeacherService _service;

        public TeacherManager(ITeacherService service, IMapper mapper) : base(service, mapper)
        {
            _service = service;
        }

        public async Task<Guid> GetTeacherIdByClassroomIdAsync(Guid classroomId)
        {
            var teacherId = await _service.GetTeacherIdByClassroomIdAsync(classroomId);
            return teacherId;
        }

        public async Task<int> GetTeachersCount()
            => await _service.GetTeachersCount();
    }
}
