using Application.Authorization.Commands.Claims;
using Application.Authorization.Services;
using Bookify_Library_mgnt.Common;
using Domain.Enums;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Handlers.Claims
{
    public class RemoveClaimFromUserCommandHandler : IRequestHandler<RemoveClaimFromUserCommand, Result<string>>
    {
        private readonly IClaimService _claimService;

        public RemoveClaimFromUserCommandHandler(IClaimService claimService)
        {
            _claimService = claimService;
        }

        public async Task<Result<string>> Handle(RemoveClaimFromUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _claimService.
                RemoveClaimFromUserAsync(command.UserId, command.ClaimType);
            if (!result.IsSuccess)
                return Result<string>.Fail(ErrorMessages.
                    OperationFailed(nameof(OperationNames.RemoveClaimFromUser), result.Errors));
            return Result<string>.Ok(result.Data);
        }
    }
}
