using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Commands.Claims
{
    public record RemoveClaimFromUserCommand(string userId, string claimType) : IRequest<Result<string>>;
}
