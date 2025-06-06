using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Commands.Claims
{
    public record AddClaimToUserCommand(
        string userId, string claimType, string claimValue) : IRequest<Result<string>>;

}
