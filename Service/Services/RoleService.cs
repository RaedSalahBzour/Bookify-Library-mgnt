using Application.Authorization.Dtos.Roles;
using Application.Authorization.Services;
using AutoMapper;
using Data.Entities;
using Data.Enums;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Service.Services;

public class RoleService(IMapper mapper, IUnitOfWork unitOfWork) : IRoleService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<List<RoleDto>> GetRolesAsync()
    {
        List<IdentityRole> roles = _unitOfWork.RoleRepository.GetRoles();
        List<RoleDto> roleDtos = _mapper.Map<List<RoleDto>>(roles);
        return roleDtos;
    }
    public async Task<RoleDto> GetRoleByIdAsync(string id)
    {
        IdentityRole? role = await _unitOfWork.RoleRepository.FindRoleByIdAsync(id);
        if (role == null)
            throw new KeyNotFoundException($"Role With Id {id} Was Not Found");
        RoleDto roleDto = _mapper.Map<RoleDto>(role);
        return roleDto;
    }

    public async Task<IdentityRole> CreateRoleAsync(CreateRoleDto CreateRoleDto)
    {
        bool roleExists = await _unitOfWork.RoleRepository.RoleExistsAsync(CreateRoleDto.RoleName);
        if (roleExists)
            throw new InvalidOperationException($"Role With Id {CreateRoleDto.RoleName} Is Already Exist");

        IdentityRole? roleEntity = _mapper.Map<IdentityRole>(CreateRoleDto);
        roleEntity.ConcurrencyStamp = Guid.NewGuid().ToString();
        IdentityResult identityResult = await _unitOfWork.RoleRepository.CreateRoleAsync(roleEntity);
        if (!identityResult.Succeeded)
            throw new InvalidOperationException(
          $"Operation '{OperationNames.CreateRole}' failed to complete.");
        return roleEntity;

    }
    public async Task<IdentityRole> UpdateRoleAsync(string id, UpdateRoleDto UpdateRoleDto)
    {
        IdentityRole? role = await _unitOfWork.RoleRepository.FindRoleByIdAsync(id);
        if (role is null)
            throw new KeyNotFoundException($"Role With Id {UpdateRoleDto.RoleName} Was Not Found");
        bool roleExists = await _unitOfWork.RoleRepository.RoleExistsAsync(UpdateRoleDto.RoleName);
        if (roleExists)
            throw new KeyNotFoundException($"Role With Id {UpdateRoleDto.RoleName} Is Already Exist");
        _mapper.Map(UpdateRoleDto, role);
        IdentityResult identityResult = await _unitOfWork.RoleRepository.UpdateRoleAsync(role);
        if (!identityResult.Succeeded)
            throw new InvalidOperationException(
          $"Operation '{OperationNames.UpdateRole}' failed to complete.");
        return role;
    }

    public async Task<IdentityRole> DeleteRoleAsync(string id)
    {
        IdentityRole? role = await _unitOfWork.RoleRepository.FindRoleByIdAsync(id);
        if (role is null)
            throw new KeyNotFoundException($"Role With Id {id} Was Not Found");

        IdentityResult identityResult = await _unitOfWork.RoleRepository.DeleteRoleAsync(role);
        if (!identityResult.Succeeded)
            throw new InvalidOperationException(
          $"Operation '{OperationNames.DeleteRole}' failed to complete.");
        return role;
    }

    public async Task<string> AddUserToRoleAsync(string userId, string roleName)
    {
        User? user = await _unitOfWork.AuthRepository.GetUserByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"User with ID '{userId}' was not found.");

        IdentityRole? role = await _unitOfWork.RoleRepository.FindRoleByNameAsync(roleName);
        if (role == null)
            throw new KeyNotFoundException($"Role with name '{roleName}' was not found.");

        IdentityResult identityResult = await _unitOfWork.RoleRepository.AddUserToRoleAsync(user, roleName);
        if (!identityResult.Succeeded)
            throw new InvalidOperationException(
          $"Operation '{OperationNames.AddUserToRole}' failed to complete.");
        string UserAssignedToRole = nameof(OperationNames.UserAssignedToRole);
        return UserAssignedToRole;
    }
    public async Task<string> RemoveUserFromRoleAsync(string userId, string roleName)
    {
        User? user = await _unitOfWork.AuthRepository.GetUserByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"User with ID '{userId}' was not found.");

        IdentityRole role = await _unitOfWork.RoleRepository.FindRoleByNameAsync(roleName);
        if (role == null)
            throw new KeyNotFoundException($"Role with name '{roleName}' was not found.");

        IdentityResult identityResult = await _unitOfWork.RoleRepository.RemoveUserFromRoleAsync(user, roleName);
        if (!identityResult.Succeeded)
            throw new InvalidOperationException(
                     $"Operation '{OperationNames.RemoveUserFromRole}' failed to complete.");

        string UserUnAssignedFromRole = nameof(OperationNames.UserUnAssignedFromRole);
        return UserUnAssignedFromRole; ;
    }
    public async Task<IList<string>> GetUserRoles(string email)
    {
        User? user = await _unitOfWork.AuthRepository.GetUserByEmailAsync(email);
        if (user == null)
            throw new KeyNotFoundException($"User with Email '{email}' was not found.");
        IList<string> userRoles = await _unitOfWork.RoleRepository.GetUserRolesAsync(user);
        return userRoles;
    }
}
