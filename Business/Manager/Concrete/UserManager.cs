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
            appUser.PasswordHash = _passwordHasher.HashPassword(appUser, "1234");
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

        public async Task<EditUserDTO> FindUserAsync(ClaimsPrincipal claims)
        {
            var user = await _userService.FindUserAsync(claims);
            var dto = _mapper.Map<EditUserDTO>(user);
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
            var token = await _userService.GenerateTokenForResetPassword(user);
            return token;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDTO dto)
        {
            var appUser = await _userService.FindUserByIdAsync(dto.Id);
            var result = await _userService.ChangePasswordAsync(appUser, dto.OldPassword, dto.NewPassword);
            return result.Succeeded;
        }

        public async Task<Guid> GetUserIdAsync(ClaimsPrincipal claimsPrincipal)
            => await _userService.GetUserIdAsync(claimsPrincipal);
    }
}
