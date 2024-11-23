using ApplicationCore.Consts;
using ApplicationCore.UserEntites.Concrete;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<List<AppRole>> GetRolesAsync()
            => await _roleManager.Roles.Where(x => x.Status != Status.Passive).ToListAsync();

        public async Task<AppRole> FindRoleAsync(Guid roleId)
            => await _roleManager.Roles.FirstOrDefaultAsync(x => x.Status != Status.Passive && x.Id == roleId);

        public async Task<bool> CheckRoleNameAsync(string roleName, Guid roleId)
        {
            var result = await _roleManager.Roles.AsNoTracking().AnyAsync(x => x.Id != roleId && string.Equals(x.Name.ToLower(), roleName.ToLower()));
            return result;
        }

        public async Task<bool> CheckRoleNameAsync(string roleName)
            => await _roleManager.RoleExistsAsync(roleName);

        public async Task<IdentityResult> AddRoleAsync(AppRole role)
            => await _roleManager.CreateAsync(role);

        public async Task<IdentityResult> UpdateRoleAsync(AppRole role)
        {
            role.UpdatedDate = DateTime.Now;
            role.Status = Status.Modified;
            return await _roleManager.UpdateAsync(role);
        }
        
        public async Task<IdentityResult> DeleteRoleAsync(AppRole role)
        {
            role.DeletedDate = DateTime.Now;
            role.Status = Status.Passive;
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<bool> CheckRoleByIdAsync(Guid roleId)
            => await _roleManager.Roles.AnyAsync(x => x.Status != Status.Passive && x.Id == roleId);

        public async Task<bool> CheckAnyUserInRole(Guid roleId)
        {
            bool result = false;
            var users = await _userManager.Users.Where(x => x.Status != Status.Passive).ToListAsync();
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
