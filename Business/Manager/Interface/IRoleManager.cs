using DTO.Concrete.RoleDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager.Interface
{
    public interface IRoleManager
    {
        Task<List<GetRoleDTO>> GetRolesAsync();
        Task<T> FindRoleByIdAsync<T>(Guid roleId);
        Task<bool> CheckRoleNameAsync(string roleName, Guid roleId);
        Task<bool> CheckRoleNameAsync(string roleName);
        Task<bool> CheckAnyUserInRoleAsync(Guid roleId);
        Task<bool> AnyRoleById(Guid roleId);
        Task<IdentityResult> CreateRoleAsync(CreateRoleDTO dto);
        Task<IdentityResult> UpdateRoleAsync(UpdateRoleDTO dto);
        Task<IdentityResult> DeleteRoleAsync(Guid id);
    }
}
