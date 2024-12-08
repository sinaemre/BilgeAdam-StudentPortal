using ApplicationCore.Entities.Concrete;
using AutoMapper;
using Business.Manager.Interface;
using DataAccess.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager.Concrete
{
    public class StudentManager : BaseManager<IStudentService, Student>, IStudentManager
    {
        private readonly IStudentService _service;
        private readonly IUserService _userService;

        public StudentManager(IStudentService service, IMapper mapper, IUserService userService) : base(service, mapper)
        {
            _service = service;
            _userService = userService;
        }

        public async Task<int> GetSuccessStudentsPercentage()
            => await _service.GetSuccessStudentsPercentage();
        public async Task<int> GetStudentsCount()
            => await _service.GetStudentsCount();


        public async Task<string> GetUserImageURL(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userService.FindUserAsync(claimsPrincipal);
            var student = await _service.GetByDefaultAsync(x => x.Email == user.Email);
            return student != null ? student.ImagePath != null ? student.ImagePath : "noimage.png" : string.Empty;
        }
    }
}
