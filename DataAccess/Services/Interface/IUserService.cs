using ApplicationCore.UserEntites.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Interface
{
    public interface IUserService
    {
        Task<List<AppUser>> GetUsersAsync();
        Task<List<AppUser>> GetUsersHasRoleAsync(string roleName);
        Task<List<AppUser>> GetUsersHasNotRoleAsync(string roleName);
        Task<List<string>> GetUserNamesHasRoleAsync(string roleName);
        Task<AppUser> FindUserByIdAsync(Guid id);
        Task<AppUser> FindUserByEmailAsync(string email);
        Task<AppUser> FindUserByUserNameAsync(string userName);
        Task<AppUser> FindUserAsync(ClaimsPrincipal user);
        Task<Guid> GetUserIdAsync(ClaimsPrincipal user);
        Task<IdentityResult> AddUserToRoleAsync(AppUser user, string roleName);
        Task<IdentityResult> RemoveUserFromRoleAsync(AppUser user, string roleName);
        Task<bool> IsUserInRoleAsync(AppUser user, string roleName);
        Task<SignInResult> LoginAsync(string userName, string password);
        Task LogoutAsync();
        Task<IdentityResult> CreateUserAsync(AppUser user);
        Task<bool> UpdateUserAsync(AppUser user);
        Task<IdentityResult> ChangePasswordAsync(AppUser user, string oldPassword, string newPassword);
        Task<string> GenerateTokenForResetPasswordAsync(AppUser appUser);
        Task<bool> IsTokenValidAsync(AppUser user, string token);
        Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword);
    }
}
