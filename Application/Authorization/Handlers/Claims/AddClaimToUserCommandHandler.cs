using Application.Authorization.Commands.Claims;
using Application.Authorization.Dtos.Claims;
using Application.Authorization.Services;
using AutoMapper;
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
    public class AddClaimToUserCommandHandler : IRequestHandler<AddClaimToUserCommand, Result<string>>
    {
        private readonly IClaimService _claimService;
        private readonly IMapper _mapper;

        public AddClaimToUserCommandHandler(IClaimService claimService, IMapper mapper)
        {
            _claimService = claimService;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(AddClaimToUserCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<AddClaimToUserDto>(command);
            var result = await _claimService.AddClaimToUserAsync(dto);
            if (!result.IsSuccess)
                return Result<string>.Fail(ErrorMessages.
                    OperationFailed(nameof(OperationNames.AddClaimToUser), result.Errors));
            return Result<string>.Ok(result.Data);
        }
    }
}
