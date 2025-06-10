using Application.Authorization.Queries.Claims;
using Application.Authorization.Services;
using MediatR;
using System.Security.Claims;

namespace Application.Authorization.Handlers.Claims;

public class GetClaimsQueryHandler(IClaimService claimService)
    : IRequestHandler<GetClaimsQuery, IList<Claim>>
{
    public async Task<IList<Claim>> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
    {
        return await claimService.GetUserClaimsAsync(request.id);

    }
}
