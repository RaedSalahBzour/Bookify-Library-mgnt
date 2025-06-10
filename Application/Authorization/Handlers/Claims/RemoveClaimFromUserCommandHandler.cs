using Application.Authorization.Commands.Claims;
using Application.Authorization.Services;
using MediatR;

namespace Application.Authorization.Handlers.Claims;

public class RemoveClaimFromUserCommandHandler : IRequestHandler<RemoveClaimFromUserCommand, string>
{
    private readonly IClaimService _claimService;

    public RemoveClaimFromUserCommandHandler(IClaimService claimService)
    {
        _claimService = claimService;
    }

    public async Task<string> Handle(RemoveClaimFromUserCommand command, CancellationToken cancellationToken)
    {
        return await _claimService.
            RemoveClaimFromUserAsync(command.UserId, command.ClaimType);

    }
}
