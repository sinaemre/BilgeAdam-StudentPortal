using ApplicationCore.UserEntites.Concrete;
using DTO.Concrete.UserDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DTO.Concrete.AccountDTO;

namespace Business.Manager.Interface
{
    public interface IUserManager
    {
        Task<List<string>> GetUserNamesHasRoleAsync(string roleName);
        Task<List<GetUserForRoleDTO>> GetUsersHasRoleAsync(string roleName);
        Task<List<GetUserForRoleDTO>> GetUsersHasNotRoleAsync(string roleName);
        Task<Guid> GetUserIdAsync(ClaimsPrincipal claimsPrincipal);
        Task<bool> AddUserToRoleAsync(Guid userId, string roleName);
        Task<bool> AddUserToRoleAsync(string email, string roleName);
        Task<bool> RemoveUserFromRoleAsync(Guid userId, string roleName);
        Task<bool> CreateUserAsync(CreateUserDTO user);
        Task<GetUserDTO> FindUserByEmailAsync(string email);
        Task<bool> LoginAsync(LoginDTO dto);
        Task LogoutAsync();
        Task<bool> IsUserInRoleAsync(string userName, string roleName);
        Task<T> FindUserAsync<T>(ClaimsPrincipal claims);
        Task<bool> UpdateUserAsync(EditUserDTO dto);
        Task<string> GenerateTokenForResetPassword(Guid userId);
        Task<bool> ChangePasswordAsync(ChangePasswordDTO dto);
        Task<bool> ChangePasswordAsync(CreatePasswordDTO dto);
        Task<bool> IsTokenValid(GetUserDTO user, string token);
        Task<bool> ResetPasswordAsync(ResetPasswordDTO dto);
    }
}
