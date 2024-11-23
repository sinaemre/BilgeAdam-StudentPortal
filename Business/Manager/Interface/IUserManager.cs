using ApplicationCore.UserEntites.Concrete;
using DTO.Concrete.UserDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager.Interface
{
    public interface IUserManager
    {
        Task<List<string>> GetUserNamesHasRoleAsync(string roleName);
        Task<List<GetUserForRoleDTO>> GetUsersHasRoleAsync(string roleName);
        Task<List<GetUserForRoleDTO>> GetUsersHasNotRoleAsync(string roleName);
        Task<bool> AddUserToRoleAsync(Guid userId, string roleName);
        Task<bool> RemoveUserFromRoleAsync(Guid userId, string roleName);
    }
}
