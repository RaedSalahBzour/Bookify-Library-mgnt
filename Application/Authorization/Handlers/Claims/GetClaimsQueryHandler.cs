using Application.Authorization.Queries.Claims;
using Application.Authorization.Services;
using MediatR;
using System.Security.Claims;

namespace Application.Authorization.Handlers.Claims;

public class GetClaimsQueryHandler : IRequestHandler<GetClaimsQuery, IList<Claim>>
{
    private readonly IClaimService _claimService;

    public GetClaimsQueryHandler(IClaimService claimService)
    {
        _claimService = claimService;
    }

    public async Task<IList<Claim>> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
    {
        return await _claimService.GetUserClaimsAsync(request.id);

    }
}
