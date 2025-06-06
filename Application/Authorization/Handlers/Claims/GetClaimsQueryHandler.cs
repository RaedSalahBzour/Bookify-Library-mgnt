using Application.Authorization.Queries.Claims;
using Application.Authorization.Services;
using Bookify_Library_mgnt.Common;
using Domain.Enums;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Handlers.Claims
{
    public class GetClaimsQueryHandler : IRequestHandler<GetClaimsQuery, Result<IList<Claim>>>
    {
        private readonly IClaimService _claimService;

        public GetClaimsQueryHandler(IClaimService claimService)
        {
            _claimService = claimService;
        }

        public async Task<Result<IList<Claim>>> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
        {
            var result = await _claimService.GetUserClaimsAsync(request.id);
            if (!result.IsSuccess)
                return Result<IList<Claim>>.Fail(ErrorMessages.
                    OperationFailed(nameof(OperationNames.GetUserClaims), result.Errors));
            return Result<IList<Claim>>.Ok(result.Data);
        }
    }
}
