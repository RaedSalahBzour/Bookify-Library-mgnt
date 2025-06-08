using Application.Authorization.Dtos.Claims;
using Application.Authorization.Services;
using Application.Common.Interfaces;
using Bookify_Library_mgnt.Common;
using Domain.Enums;
using Domain.Shared;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClaimService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IList<Claim>>> GetUserClaimsAsync(string userId)
        {
            var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return Result<IList<Claim>>.Fail(ErrorMessages.NotFoundById(userId));
            }
            var userClaims = await _unitOfWork.AuthRepository.GetUserClaimsAsync(user);
            return Result<IList<Claim>>.Ok(userClaims);
        }
        public async Task<Result<string>> AddClaimToUserAsync(AddClaimToUserDto addClaimDto)
        {
            var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(addClaimDto.userId);
            if (user == null)
            {
                return Result<string>.Fail(ErrorMessages.NotFoundById(addClaimDto.userId));
            }
            var claim = new Claim(addClaimDto.claimType, addClaimDto.claimValue);
            var result = await _unitOfWork.ClaimRepository.AddClaimToUser(user, claim);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Result<string>.Fail(ErrorMessages
                    .OperationFailed(nameof(OperationNames.AddClaimToUser), errors));
            }
            return Result<string>.Ok("Claim Added");
        }

        public async Task<Result<string>> RemoveClaimFromUserAsync(string userId, string claimType)
        {
            var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return Result<string>.Fail(ErrorMessages.NotFoundById(userId));
            }
            var claims = await _unitOfWork.AuthRepository.GetUserClaimsAsync(user);
            var claimToRemove = claims.FirstOrDefault(c => c.Type == claimType);

            if (claimToRemove == null)
            {
                return Result<string>.Fail(ErrorMessages.NotFoundByName(claimType));
            }
            var result = await _unitOfWork.ClaimRepository.RemoveClaimFromUser(user, claimToRemove);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Result<string>.Fail(ErrorMessages
                    .OperationFailed(nameof(OperationNames.RemoveUserFromRole), errors));
            }
            return Result<string>.Ok(nameof(OperationNames.CLaimRemoved));

        }


    }
}
