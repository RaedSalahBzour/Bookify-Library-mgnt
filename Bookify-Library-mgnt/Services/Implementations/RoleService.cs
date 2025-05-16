using AutoMapper;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Dtos.Categories;
using Bookify_Library_mgnt.Dtos.Roles;
using Bookify_Library_mgnt.Enums;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Bookify_Library_mgnt.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateRoleDto> _createValidator;
        private readonly IValidator<UpdateRoleDto> _updateValidator;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleService(
            IMapper mapper, IValidator<CreateRoleDto> createValidator,
            IValidator<UpdateRoleDto> updateValidator,
            RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<PagedResult<RoleDto>> GetRolesAsync(int pageNumber = 1, int pageSize = 10)
        {
            var roles = await _roleManager.Roles.ToPaginationForm(pageNumber, pageSize);
            var rolesDto = _mapper.Map<IEnumerable<RoleDto>>(roles.Items);
            return new PagedResult<RoleDto>
            {
                TotalCount = roles.TotalCount,
                Items = rolesDto,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<Result<RoleDto>> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return Result<RoleDto>.Fail(ErrorMessages.NotFoundById(id));
            }
            var roleDto = _mapper.Map<RoleDto>(role);
            return Result<RoleDto>.Ok(roleDto);
        }

        public async Task<Result<IdentityRole>> CreateRoleAsync(CreateRoleDto roleDto)
        {
            var validationResult = await _createValidator.ValidateAsync(roleDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return Result<IdentityRole>.Fail(errorMessages);
            }
            var role = await _roleManager.RoleExistsAsync(roleDto.RoleName);
            if (role)
            {
                return Result<IdentityRole>.Fail(ErrorMessages.AlreadyExist(roleDto.RoleName));
            }
            var roleEntity = _mapper.Map<IdentityRole>(roleDto);
            roleEntity.ConcurrencyStamp = Guid.NewGuid().ToString();
            var result = await _roleManager.CreateAsync(roleEntity);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Result<IdentityRole>.Fail(ErrorMessages.OperationFailed(nameof(OperationNames.CreateRole), errors));

            }
            return Result<IdentityRole>.Ok(roleEntity);

        }
        public async Task<Result<IdentityRole>> UpdateRoleAsync(string id, UpdateRoleDto roleDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(roleDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return Result<IdentityRole>.Fail(errorMessages);
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
            {
                return Result<IdentityRole>.Fail(ErrorMessages.NotFoundById(id));
            }
            var roleByName = await _roleManager.RoleExistsAsync(roleDto.RoleName);
            if (roleByName)
            {
                return Result<IdentityRole>.Fail(ErrorMessages.AlreadyExist(roleDto.RoleName));
            }
            _mapper.Map(roleDto, role);
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Result<IdentityRole>.Fail(ErrorMessages.OperationFailed(nameof(OperationNames.UpdateRole), errors));

            }
            return Result<IdentityRole>.Ok(role);
        }

        public async Task<Result<IdentityRole>> DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
            {
                return Result<IdentityRole>.Fail(ErrorMessages.NotFoundById(id));
            }
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Result<IdentityRole>.Fail(ErrorMessages.OperationFailed(nameof(OperationNames.DeleteRole), errors));

            }
            return Result<IdentityRole>.Ok(role);
        }

        public async Task<Result<string>> AddUserToRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var role = await _roleManager.FindByNameAsync(roleName);
            if (user == null || role == null)
                return Result<string>.Fail(user == null ?
                    ErrorMessages.NotFoundById(userId) : ErrorMessages.NotFoundByName(roleName));

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Result<string>.Fail(ErrorMessages.OperationFailed(nameof(OperationNames.AddUserToRole), errors));

            }
            return Result<string>.Ok("added");
        }
        public async Task<Result<string>> RemoveUserFromRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var role = await _roleManager.FindByNameAsync(roleName);
            if (user == null || role == null)
                return Result<string>.Fail(user == null ?
                    ErrorMessages.NotFoundById(userId) : ErrorMessages.NotFoundByName(roleName));

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Result<string>.Fail(ErrorMessages.OperationFailed(nameof(OperationNames.RemoveUserFromRole), errors));

            }
            return Result<string>.Ok("Removed");
        }
        public async Task<Result<IEnumerable<string>>> GetUserRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Result<IEnumerable<string>>.Fail(ErrorMessages.NotFoundByName(email));
            var roles = await _userManager.GetRolesAsync(user);
            return Result<IEnumerable<string>>.Ok(roles);
        }
    }
}
