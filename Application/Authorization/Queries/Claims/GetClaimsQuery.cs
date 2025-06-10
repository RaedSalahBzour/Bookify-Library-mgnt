using MediatR;
using System.Security.Claims;

namespace Application.Authorization.Queries.Claims;

public record GetClaimsQuery(string id) : IRequest<IList<Claim>>;
