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
using DataAccess.Context.IdentityContext;

namespace DataAccess.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppIdentityDbContext _context;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppIdentityDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
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
        {
            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email && x.Status != Status.Passive);
            return user;
        }

        public async Task<AppUser> FindUserByIdAsync(Guid id)
        {
            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Status != Status.Passive);
            return user;
        }

        public async Task<AppUser> FindUserByUserNameAsync(string userName)
        {
            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == userName && x.Status != Status.Passive);
            return user;
        }

        public async Task<IdentityResult> AddUserToRoleAsync(AppUser user, string roleName)
        {
            var trackedUser = _context.Users.Local.FirstOrDefault(x => x.Id == user.Id);
            if (trackedUser != null)
            {
                _context.Entry(trackedUser).State = EntityState.Detached;
            }
            
            return await _userManager.AddToRoleAsync(user, roleName);;
        }

        public async Task<IdentityResult> RemoveUserFromRoleAsync(AppUser user, string roleName)
            => await _userManager.RemoveFromRoleAsync(user, roleName);

        public async Task<bool> IsUserInRoleAsync(AppUser user, string roleName)
            => await _userManager.IsInRoleAsync(user, roleName);

        public async Task<IdentityResult> CreateUserAsync(AppUser user)
            => await _userManager.CreateAsync(user);

        public async Task<bool> UpdateUserAsync(AppUser user)
        {
            var entity = await _userManager.FindByIdAsync(user.Id.ToString());
            entity.Email = user.Email;
            entity.UpdatedDate = DateTime.Now;
            entity.Status = Status.Modified;
            _context.Entry(entity).OriginalValues.SetValues(entity); // Optimistic Concurrency'nin Devre Dışı Bırakılması
            _context.Update(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IdentityResult> ChangePasswordAsync(AppUser user, string oldPassword, string newPassword)
            => await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        public async Task<SignInResult> LoginAsync(string userName, string password)
            => await _signInManager.PasswordSignInAsync(userName, password, false, false);

        public async Task LogoutAsync()
            => await _signInManager.SignOutAsync();

        public async Task<string> GenerateTokenForResetPasswordAsync(AppUser appUser)
            => await _userManager.GeneratePasswordResetTokenAsync(appUser);

        public async Task<bool> IsTokenValidAsync(AppUser user, string token)
        {
            var tokenProvider = _userManager.Options.Tokens.PasswordResetTokenProvider;
            var result = await _userManager.VerifyUserTokenAsync(user, tokenProvider, "ResetPassword", token);
            return result;
        }

        public async Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword)
            => await _userManager.ResetPasswordAsync(user, token, newPassword);
    }
}
