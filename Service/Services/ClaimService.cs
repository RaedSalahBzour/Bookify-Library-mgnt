using Application.Authorization.Dtos.Claims;
using Application.Authorization.Services;
using Data.Enums;
using Data.Interfaces;
using System.Security.Claims;

namespace Service.Services;

public class ClaimService : IClaimService
{
    private readonly IUnitOfWork _unitOfWork;

    public ClaimService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<Claim>> GetUserClaimsAsync(string userId)
    {
        var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"User With Id {userId} Was Not Found");
        return await _unitOfWork.AuthRepository.GetUserClaimsAsync(user);

    }
    public async Task<string> AddClaimToUserAsync(AddClaimToUserDto addClaimDto)
    {
        var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(addClaimDto.userId);
        if (user == null)
            throw new KeyNotFoundException($"User With Id {addClaimDto.userId} Was Not Found");
        var claim = new Claim(addClaimDto.claimType, addClaimDto.claimValue);
        var result = await _unitOfWork.ClaimRepository.AddClaimToUser(user, claim);
        if (!result.Succeeded)
            throw new InvalidOperationException(
              $"Operation '{OperationNames.AddClaimToUser}' failed to complete.");

        return nameof(OperationNames.ClaimAdded);
    }

    public async Task<string> RemoveClaimFromUserAsync(string userId, string claimType)
    {
        var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"User With Id {userId} Was Not Found");

        var claims = await _unitOfWork.AuthRepository.GetUserClaimsAsync(user);
        var claimToRemove = claims.FirstOrDefault(c => c.Type == claimType);

        if (claimToRemove == null)
            throw new KeyNotFoundException($"Claim With Type {claimType} Was Not Found");

        var result = await _unitOfWork.ClaimRepository.RemoveClaimFromUser(user, claimToRemove);
        if (!result.Succeeded)
            throw new InvalidOperationException(
                   $"Operation '{OperationNames.RemoveClaimFromUser}' failed to complete.");
        return nameof(OperationNames.CLaimRemoved);

    }


}
