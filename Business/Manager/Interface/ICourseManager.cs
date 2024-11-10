﻿using ApplicationCore.Entities.Concrete;
using DataAccess.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager.Interface
{
    public interface ICourseManager : IBaseManager<ICourseService, Course>
    {
        Task<Guid> GetCourseIdByClassroomIdAsync(Guid classroomId);
    }
}
