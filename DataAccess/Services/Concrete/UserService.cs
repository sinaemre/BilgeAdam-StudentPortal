using ApplicationCore.Consts;
using ApplicationCore.UserEntites.Concrete;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<List<AppUser>> GetUsersAsync()
            => await _userManager.Users.Where(x => x.Status != Status.Passive).ToListAsync();

        public async Task<List<AppUser>> GetUsersHasNotRoleAsync(string roleName)
        {

            var users = await GetUsersAsync();
            var usersHasNotRole = new List<AppUser>();

            foreach (var user in users)
            {
                if (!await _userManager.IsInRoleAsync(user, roleName))
                {
                    usersHasNotRole.Add(user);
                }
            }

            return usersHasNotRole;
        }

        public async Task<List<AppUser>> GetUsersHasRoleAsync(string roleName)
        {
            var users = await GetUsersAsync();
            var usersHasRole = new List<AppUser>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, roleName))
                {
                    usersHasRole.Add(user);
                }
            }

            return usersHasRole;
        }
        public async Task<List<string>> GetUserNamesHasRoleAsync(string roleName)
        {
            var users = await GetUsersAsync();
            var userNamesHasRole = new List<string>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, roleName))
                {
                    userNamesHasRole.Add(user.UserName);
                }
            }

            return userNamesHasRole;
        }


        public async Task<Guid> GetUserIdAsync(ClaimsPrincipal claims)
        {
            var user = await _userManager.GetUserAsync(claims);
            return user.Id;
        }

        public async Task<AppUser> FindUserAsync(ClaimsPrincipal claims)
            => await _userManager.GetUserAsync(claims);

        public async Task<AppUser> FindUserByEmailAsync(string email)
            => await _userManager.FindByEmailAsync(email);

        public async Task<AppUser> FindUserByIdAsync(Guid id)
            => await _userManager.FindByIdAsync(id.ToString());

        public async Task<AppUser> FindUserByUserNameAsync(string userName)
            => await _userManager.FindByNameAsync(userName);


        public async Task<IdentityResult> AddUserToRoleAsync(AppUser user, string roleName)
            => await _userManager.AddToRoleAsync(user, roleName);

        public async Task<IdentityResult> RemoveUserFromRoleAsync(AppUser user, string roleName)
            => await _userManager.RemoveFromRoleAsync(user, roleName);

        public async Task<bool> IsUserInRoleAsync(AppUser user, string roleName)
            => await _userManager.IsInRoleAsync(user, roleName);

        public async Task<IdentityResult> CreateUserAsync(AppUser user)
            => await _userManager.CreateAsync(user);

        public async Task<IdentityResult> UpdateUserAsync(AppUser user)
        {
            user.UpdatedDate = DateTime.Now;
            user.Status = ApplicationCore.Consts.Status.Modified;
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(AppUser user, string oldPassword, string newPassword)
            => await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        public async Task<SignInResult> LoginAsync(string userName, string password)
            => await _signInManager.PasswordSignInAsync(userName, password, false, false);

        public async Task LogoutAsync()
            => await _signInManager.SignOutAsync();
    }
}
