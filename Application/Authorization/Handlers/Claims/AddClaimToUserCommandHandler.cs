using Application.Authorization.Commands.Claims;
using Application.Authorization.Dtos.Claims;
using Application.Authorization.Services;
using AutoMapper;
using MediatR;

namespace Application.Authorization.Handlers.Claims;

public class AddClaimToUserCommandHandler(IClaimService claimService, IMapper mapper)
    : IRequestHandler<AddClaimToUserCommand, string>
{
    public async Task<string> Handle(AddClaimToUserCommand command, CancellationToken cancellationToken)
    {
        AddClaimToUserDto addClaimToUserDto = mapper.Map<AddClaimToUserDto>(command);
        return await claimService.AddClaimToUserAsync(addClaimToUserDto);

    }
}
