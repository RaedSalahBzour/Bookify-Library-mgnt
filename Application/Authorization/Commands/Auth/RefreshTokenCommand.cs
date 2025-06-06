using Application.Authorization.Dtos.Token;
using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Commands.Auth
{
    public class RefreshTokenCommand : IRequest<Result<TokenResponseDto?>>
    {
        public string UserId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
