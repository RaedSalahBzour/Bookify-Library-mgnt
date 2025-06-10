using Application.Authorization.Commands.Claims;
using Application.Authorization.Services;
using MediatR;

namespace Application.Authorization.Handlers.Claims;

public class RemoveClaimFromUserCommandHandler(IClaimService claimService)
    : IRequestHandler<RemoveClaimFromUserCommand, string>
{
    public async Task<string> Handle(RemoveClaimFromUserCommand command, CancellationToken cancellationToken)
    {
        return await claimService.
            RemoveClaimFromUserAsync(command.UserId, command.ClaimType);

    }
}
