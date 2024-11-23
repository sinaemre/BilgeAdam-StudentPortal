using ApplicationCore.UserEntites.Concrete;
using AutoMapper;
using Business.Manager.Interface;
using DataAccess.Services.Interface;
using DTO.Concrete.UserDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return result.Succeeded ? true : false;
        }

        public async Task<bool> RemoveUserFromRoleAsync(Guid userId, string roleName)
        {
            var user = await _userService.FindUserByIdAsync(userId);
            var result = await _userService.RemoveUserFromRoleAsync(user, roleName);
            return result.Succeeded ? true : false;
        }
    }
}
