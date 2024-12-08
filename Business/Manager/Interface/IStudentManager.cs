using ApplicationCore.Entities.Concrete;
using DataAccess.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager.Interface
{
    public interface IStudentManager : IBaseManager<IStudentService, Student>
    {
        Task<string> GetUserImageURL(ClaimsPrincipal claimsPrincipal);
        Task<int> GetSuccessStudentsPercentage();
        Task<int> GetStudentsCount();
    }
}
