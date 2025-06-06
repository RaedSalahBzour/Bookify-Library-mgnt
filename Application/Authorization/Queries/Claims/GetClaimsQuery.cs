using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Queries.Claims
{
    public record GetClaimsQuery(string id) : IRequest<Result<IList<Claim>>>;

}
