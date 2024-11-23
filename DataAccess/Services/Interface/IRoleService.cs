using ApplicationCore.UserEntites.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Interface
{
    public interface IRoleService
    {
        Task<List<AppRole>> GetRolesAsync();
        Task<AppRole> FindRoleAsync(Guid roleId);
        Task<bool> CheckRoleNameAsync(string roleName, Guid roleId);
        Task<bool> CheckRoleNameAsync(string roleName);
        Task<bool> CheckRoleByIdAsync(Guid roleId);
        Task<bool> CheckAnyUserInRole(Guid roleId);
        Task<IdentityResult> AddRoleAsync(AppRole role);
        Task<IdentityResult> UpdateRoleAsync(AppRole role);
        Task<IdentityResult> DeleteRoleAsync(AppRole role);

    }
}
