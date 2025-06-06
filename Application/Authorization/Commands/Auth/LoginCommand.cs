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
    public class LoginCommand : IRequest<Result<TokenResponseDto?>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
