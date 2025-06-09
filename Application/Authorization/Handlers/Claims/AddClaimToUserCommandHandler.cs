using Application.Authorization.Commands.Claims;
using Application.Authorization.Dtos.Claims;
using Application.Authorization.Services;
using AutoMapper;
using MediatR;

namespace Application.Authorization.Handlers.Claims
{
    public class AddClaimToUserCommandHandler : IRequestHandler<AddClaimToUserCommand, string>
    {
        private readonly IClaimService _claimService;
        private readonly IMapper _mapper;

        public AddClaimToUserCommandHandler(IClaimService claimService, IMapper mapper)
        {
            _claimService = claimService;
            _mapper = mapper;
        }

        public async Task<string> Handle(AddClaimToUserCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<AddClaimToUserDto>(command);
            return await _claimService.AddClaimToUserAsync(dto);

        }
    }
}
