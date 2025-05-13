using AutoMapper;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Dtos.Categories;
using Bookify_Library_mgnt.Dtos.Roles;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Bookify_Library_mgnt.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateRoleDto> _createValidator;
        private readonly IValidator<UpdateRoleDto> _updateValidator;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(IRoleRepository roleRepository,
            IMapper mapper, IValidator<CreateRoleDto> createValidator,
            IValidator<UpdateRoleDto> updateValidator,
            RoleManager<IdentityRole> roleManager)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _roleManager = roleManager;
        }

        public async Task<PagedResult<RoleDto>> GetRolesAsync(int pageNumber = 1, int pageSize = 10)
        {
            var roles = _roleRepository.GetRoles();
            var paginatedRoles = await roles.ToPaginationForm(pageNumber, pageSize);
            var rolesDto = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return new PagedResult<RoleDto>
            {
                TotalCount = paginatedRoles.TotalCount,
                Items = rolesDto,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<Result<RoleDto>> GetRoleByIdAsync(string id)
        {
            var role = await _roleRepository.GetRoleById(id);
            if (role == null)
            {
                return Result<RoleDto>.Fail(ErrorMessages.NotFound(id));
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
            var roleEntity = new IdentityRole
            {
                Name = roleDto.RoleName,
                NormalizedName = roleDto.RoleName.ToUpper()
            }; await _roleRepository.CreateRoleAsync(roleEntity);
            await _roleRepository.SaveChangesAsync();
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
                return Result<IdentityRole>.Fail(ErrorMessages.NotFound(id));
            }
            var roleByName = await _roleManager.RoleExistsAsync(roleDto.RoleName);
            if (roleByName)
            {
                return Result<IdentityRole>.Fail(ErrorMessages.AlreadyExist(roleDto.RoleName));
            }
            role.Name = roleDto.RoleName;
            role.NormalizedName = roleDto.RoleName.ToUpper();
            await _roleRepository.UpdateRoleAsync(role);
            await _roleRepository.SaveChangesAsync();
            return Result<IdentityRole>.Ok(role);
        }

        public async Task<Result<IdentityRole>> DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
            {
                return Result<IdentityRole>.Fail(ErrorMessages.NotFound(id));
            }
            await _roleRepository.DeleteRoleAsync(role);
            await _roleRepository.SaveChangesAsync();
            return Result<IdentityRole>.Ok(role);
        }
    }
}
