using Application.Authorization.Dtos.Roles;
using Application.Authorization.Services;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RoleDto>> GetRolesAsync()
        {
            var roles = _unitOfWork.RoleRepository.GetRoles();
            return _mapper.Map<List<RoleDto>>(roles);
        }
        public async Task<RoleDto> GetRoleByIdAsync(string id)
        {
            var role = await _unitOfWork.RoleRepository.FindRoleByIdAsync(id);
            if (role == null)
                throw new KeyNotFoundException($"Role With Id {id} Was Not Found");

            return _mapper.Map<RoleDto>(role);

        }

        public async Task<IdentityRole> CreateRoleAsync(CreateRoleDto roleDto)
        {
            var role = await _unitOfWork.RoleRepository.RoleExistsAsync(roleDto.RoleName);
            if (role)
                throw new InvalidOperationException($"Role With Id {roleDto.RoleName} Is Already Exist");

            var roleEntity = _mapper.Map<IdentityRole>(roleDto);
            roleEntity.ConcurrencyStamp = Guid.NewGuid().ToString();
            var result = await _unitOfWork.RoleRepository.CreateRoleAsync(roleEntity);
            if (!result.Succeeded)
                throw new InvalidOperationException(
              $"Operation '{OperationNames.CreateRole}' failed to complete.");
            return roleEntity;

        }
        public async Task<IdentityRole> UpdateRoleAsync(string id, UpdateRoleDto roleDto)
        {
            var role = await _unitOfWork.RoleRepository.FindRoleByIdAsync(id);
            if (role is null)
                throw new KeyNotFoundException($"Role With Id {roleDto.RoleName} Was Not Found");
            var roleByName = await _unitOfWork.RoleRepository.RoleExistsAsync(roleDto.RoleName);
            if (roleByName)
                throw new KeyNotFoundException($"Role With Id {roleDto.RoleName} Is Already Exist");
            _mapper.Map(roleDto, role);
            var result = await _unitOfWork.RoleRepository.UpdateRoleAsync(role);
            if (!result.Succeeded)
                throw new InvalidOperationException(
              $"Operation '{OperationNames.UpdateRole}' failed to complete.");
            return role;
        }

        public async Task<IdentityRole> DeleteRoleAsync(string id)
        {
            var role = await _unitOfWork.RoleRepository.FindRoleByIdAsync(id);
            if (role is null)
                throw new KeyNotFoundException($"Role With Id {id} Was Not Found");

            var result = await _unitOfWork.RoleRepository.DeleteRoleAsync(role);
            if (!result.Succeeded)
                throw new InvalidOperationException(
              $"Operation '{OperationNames.DeleteRole}' failed to complete.");
            return role;
        }

        public async Task<string> AddUserToRoleAsync(string userId, string roleName)
        {
            var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID '{userId}' was not found.");

            var role = await _unitOfWork.RoleRepository.FindRoleByNameAsync(roleName);
            if (role == null)
                throw new KeyNotFoundException($"Role with name '{roleName}' was not found.");

            var result = await _unitOfWork.RoleRepository.AddUserToRoleAsync(user, roleName);
            if (!result.Succeeded)
                throw new InvalidOperationException(
              $"Operation '{OperationNames.AddUserToRole}' failed to complete.");
            return "added";
        }
        public async Task<string> RemoveUserFromRoleAsync(string userId, string roleName)
        {
            var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID '{userId}' was not found.");

            var role = await _unitOfWork.RoleRepository.FindRoleByNameAsync(roleName);
            if (role == null)
                throw new KeyNotFoundException($"Role with name '{roleName}' was not found.");

            var result = await _unitOfWork.RoleRepository.RemoveUserFromRoleAsync(user, roleName);
            if (!result.Succeeded)
                throw new InvalidOperationException(
                         $"Operation '{OperationNames.RemoveUserFromRole}' failed to complete.");

            return nameof(OperationNames.RoleRemoved);
        }
        public async Task<IList<string>> GetUserRoles(string email)
        {
            var user = await _unitOfWork.AuthRepository.GetUserByEmailAsync(email);
            if (user == null)
                throw new KeyNotFoundException($"User with Email '{email}' was not found.");
            return await _unitOfWork.RoleRepository.GetUserRolesAsync(user);

        }
    }
}
