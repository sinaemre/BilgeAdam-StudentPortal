using ApplicationCore.UserEntites.Concrete;
using AutoMapper;
using Business.Manager.Interface;
using DataAccess.Services.Interface;
using DTO.Concrete.RoleDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager.Concrete
{
    public class RoleManager : IRoleManager
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleManager(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<bool> AnyRoleById(Guid roleId)
            => await _roleService.CheckRoleByIdAsync(roleId);

        public async Task<bool> CheckAnyUserInRoleAsync(Guid roleId)
            => await _roleService.CheckAnyUserInRole(roleId);

        public async Task<bool> CheckRoleNameAsync(string roleName, Guid roleId)
            => await _roleService.CheckRoleNameAsync(roleName, roleId);

        public async Task<bool> CheckRoleNameAsync(string roleName)
            => await _roleService.CheckRoleNameAsync(roleName);

        public async Task<IdentityResult> CreateRoleAsync(CreateRoleDTO dto)
        {
            var role = _mapper.Map<AppRole>(dto);
            var result = await _roleService.AddRoleAsync(role);
            return result;
        }

        public async Task<IdentityResult> DeleteRoleAsync(Guid id)
        {
            var role = await _roleService.FindRoleAsync(id);
            var result = await _roleService.DeleteRoleAsync(role);
            return result;
        }

        public async Task<T> FindRoleByIdAsync<T>(Guid roleId)
        {
            var role = await _roleService.FindRoleAsync(roleId);
            var dto = _mapper.Map<T>(role);
            return dto;
        }

        public async Task<List<GetRoleDTO>> GetRolesAsync()
        {
            var roles = await _roleService.GetRolesAsync();
            var dto = _mapper.Map<List<GetRoleDTO>>(roles);
            return dto;
        }

        public async Task<IdentityResult> UpdateRoleAsync(UpdateRoleDTO dto)
        {
            var role = _mapper.Map<AppRole>(dto);
            var result = await _roleService.UpdateRoleAsync(role);
            return result;
        }
    }
}
