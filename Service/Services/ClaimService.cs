using Application.Authorization.Dtos.Claims;
using Application.Authorization.Services;
using Data.Entities;
using Data.Enums;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Service.Exceptions;
using System.Security.Claims;

namespace Service.Services;

public class ClaimService(IUnitOfWork unitOfWork) : IClaimService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IList<Claim>> GetUserClaimsAsync(string userId)
    {
        User? user = await _unitOfWork.AuthRepository.GetUserByIdAsync(userId);
        if (user == null)
            throw ExceptionManager
                .ReturnNotFound("User Not Found", $"User With Id {userId} Was Not Found");
        IList<Claim> userClaims = await _unitOfWork.AuthRepository.GetUserClaimsAsync(user);
        return userClaims;
    }
    public async Task<string> AddClaimToUserAsync(AddClaimToUserDto addClaimDto)
    {
        User? user = await _unitOfWork.AuthRepository.GetUserByIdAsync(addClaimDto.userId);
        if (user == null)
            throw ExceptionManager
                .ReturnNotFound("User Not Found", $"User With Id {addClaimDto.userId} Was Not Found");
        Claim? claim = new Claim(addClaimDto.claimType, addClaimDto.claimValue);
        IdentityResult identityResult = await _unitOfWork.ClaimRepository.AddClaimToUser(user, claim);
        if (!identityResult.Succeeded)
            throw ExceptionManager.ReturnInternalServerError(
        $"'{OperationNames.AddClaimToUser}' failed to complete.",
        $"Something went wrong while trying to '{OperationNames.AddClaimToUser}' Please try again later."
           );

        string added = nameof(OperationNames.ClaimAdded);
        return added;
    }

    public async Task<string> RemoveClaimFromUserAsync(string userId, string claimType)
    {
        User? user = await _unitOfWork.AuthRepository.GetUserByIdAsync(userId);
        if (user == null)
            throw ExceptionManager
                .ReturnNotFound("User Not Found", $"User With Id {userId} Was Not Found");

        IList<Claim> claims = await _unitOfWork.AuthRepository.GetUserClaimsAsync(user);
        Claim? claimToRemove = claims.FirstOrDefault(c => c.Type == claimType);

        if (claimToRemove == null)
            throw ExceptionManager
                .ReturnNotFound("Claim Not Found", $"Claim With Type {claimType} Was Not Found");
        IdentityResult identityResult = await _unitOfWork.ClaimRepository.RemoveClaimFromUser(user, claimToRemove);
        if (!identityResult.Succeeded)
            throw ExceptionManager.ReturnInternalServerError(
        $"'{OperationNames.RemoveClaimFromUser}' failed to complete.",
        $"Something went wrong while trying to '{OperationNames.RemoveClaimFromUser}' Please try again later."
           );
        string removed = nameof(OperationNames.ClaimRemoved);
        return removed;

    }


}
