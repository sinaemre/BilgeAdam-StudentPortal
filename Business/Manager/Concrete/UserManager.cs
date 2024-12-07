using ApplicationCore.UserEntites.Concrete;
using AutoMapper;
using Business.Manager.Interface;
using DataAccess.Services.Interface;
using DTO.Concrete.UserDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Business.ExtensionMethods;
using DTO.Concrete.AccountDTO;

namespace Business.Manager.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public UserManager(IUserService userService, IMapper mapper, IPasswordHasher<AppUser> passwordHasher)
        {
            _userService = userService;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

      

        public async Task<List<string>> GetUserNamesHasRoleAsync(string roleName)
            => await _userService.GetUserNamesHasRoleAsync(roleName);

        public async Task<List<GetUserForRoleDTO>> GetUsersHasNotRoleAsync(string roleName)
        {
            var users = await _userService.GetUsersHasNotRoleAsync(roleName);
            var dto = _mapper.Map<List<GetUserForRoleDTO>>(users);
            return dto;
        }

        public async Task<List<GetUserForRoleDTO>> GetUsersHasRoleAsync(string roleName)
        {
            var users = await _userService.GetUsersHasRoleAsync(roleName);
            var dto = _mapper.Map<List<GetUserForRoleDTO>>(users);
            return dto;
        }

        public async Task<bool> AddUserToRoleAsync(Guid userId, string roleName)
        {
            var user = await _userService.FindUserByIdAsync(userId);
            var result = await _userService.AddUserToRoleAsync(user, roleName);
            return result.Succeeded;
        }
        
        public async Task<bool> AddUserToRoleAsync(string email, string roleName)
        {
            var user = await _userService.FindUserByEmailAsync(email);
            var result = await _userService.AddUserToRoleAsync(user, roleName);
            return result.Succeeded;
        }
        
        public async Task<bool> RemoveUserFromRoleAsync(Guid userId, string roleName)
        {
            var user = await _userService.FindUserByIdAsync(userId);
            var result = await _userService.RemoveUserFromRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<bool> CreateUserAsync(CreateUserDTO user)
        {
            var appUser = _mapper.Map<AppUser>(user);
            var password = GenerateRandomPassword(12);
            appUser.FirstPassword = password;
            appUser.PasswordHash = _passwordHasher.HashPassword(appUser, password);
            appUser.UserName = CreateUserName(user.FirstName, user.LastName);
            var result = await _userService.CreateUserAsync(appUser);
            return result.Succeeded;
        }

        public async Task<GetUserDTO> FindUserByEmailAsync(string email)
        {
            var user = await _userService.FindUserByEmailAsync(email);
            var dto = _mapper.Map<GetUserDTO>(user);
            return dto;
        }

        public async Task<bool> LoginAsync(LoginDTO dto)
        {
            var result = await _userService.LoginAsync(dto.UserName, dto.Password);
            return result.Succeeded;
        }

        public async Task LogoutAsync()
            => await _userService.LogoutAsync();

        public async Task<bool> IsUserInRoleAsync(string userName, string roleName)
        {
            var user = await _userService.FindUserByUserNameAsync(userName);
            var result = await _userService.IsUserInRoleAsync(user, roleName);
            return result;
        }

        public async Task<T> FindUserAsync<T>(ClaimsPrincipal claims)
        {
            var user = await _userService.FindUserAsync(claims);
            var dto = _mapper.Map<T>(user);
            return dto;
        }

        public async Task<bool> UpdateUserAsync(EditUserDTO dto)
        {
            var user = _mapper.Map<AppUser>(dto);
            var result = await _userService.UpdateUserAsync(user);
            return result;
        }

        private string CreateUserName(string firstName, string lastName)
        {
            //sina emre bekar=> UserName = sinaemre.bekar;
            var name = firstName.ChangeCharacters();
            var surname = lastName.ChangeCharacters();

            var nameChar = name.Split(' ');
            var surnameChar = surname.Split(' ');
            
            var userNamePart1 = "";
            if (name.Trim().Contains(' '))
            {
                for (var i = 0; i < nameChar.Length; i++)
                {
                    userNamePart1 += name.Split(' ')[i];
                }
            }
            else
            {
                userNamePart1 = name;
            }
            var userNamePart2 = "";
            if (name.Trim().Contains(' '))
            {
                for (var i = 0; i < surnameChar.Length; i++)
                {
                    userNamePart2 += surnameChar[i];
                }
            }
            else
            {
                userNamePart2 = surname;
            }
            
            var userName = $"{userNamePart1}.{userNamePart2}";
            return userName;
        }

        public async Task<string> GenerateTokenForResetPassword(Guid userId)
        {
            var user = await _userService.FindUserByIdAsync(userId);
            var token = await _userService.GenerateTokenForResetPasswordAsync(user);
            return token;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDTO dto)
        {
            var appUser = await _userService.FindUserByIdAsync(dto.Id);
            var result = await _userService.ChangePasswordAsync(appUser, dto.OldPassword, dto.NewPassword);
            return result.Succeeded;
        }

        public async Task<bool> ChangePasswordAsync(CreatePasswordDTO dto)
        {
            var appUser = await _userService.FindUserByEmailAsync(dto.Email);
            var result = await _userService.ChangePasswordAsync(appUser, dto.OldPassword, dto.NewPassword);
            if (result.Succeeded)
            {
                appUser.HasPasswordChanged = true;
                appUser.FirstPassword = string.Empty;
                var resultUpdate = await _userService.UpdateUserAsync(appUser);
                return resultUpdate;
            }
            return result.Succeeded;
        }

        public async Task<Guid> GetUserIdAsync(ClaimsPrincipal claimsPrincipal)
            => await _userService.GetUserIdAsync(claimsPrincipal);

        public async Task<bool> IsTokenValid(GetUserDTO user, string token)
        {
            var appUser = await _userService.FindUserByEmailAsync(user.Email);
            var result = await _userService.IsTokenValidAsync(appUser, token);
            return result;
        }

        private string GenerateRandomPassword(int length = 12)
        {
            const string lowerCase = "abcdefghijklmnopqrstvwyz";
            const string upperCase = "ABCDEFGHIJKLMNOPQRSTVWYZ";
            const string digits = "0123456789";
            const string specialChars = "!@#$%&.*()/-+=<>?,-_";

            string allChars = lowerCase + upperCase + digits + specialChars;

            var random = new Random();
            var password = new char[length];

            password[0] = lowerCase[random.Next(lowerCase.Length)];
            password[1] = upperCase[random.Next(upperCase.Length)];
            password[2] = digits[random.Next(digits.Length)];
            password[3] = specialChars[random.Next(specialChars.Length)];

            for (int i = 4; i < length; i++)
            {
                password[i] = allChars[random.Next(allChars.Length)];
            }

            return new string(password);
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDTO dto)
        {
            var user = await _userService.FindUserByEmailAsync(dto.Email);
            var result = await _userService.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            return result.Succeeded;
        }
    }
}
