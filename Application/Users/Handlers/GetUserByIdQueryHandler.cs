using Application.Users.Dtos;
using Application.Users.Queries;
using Application.Users.Services;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Users.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        private readonly IAuthService _authService;

        public GetUserByIdQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<Result<UserDto>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _authService.GetUserByIdAsync(query.id);
            if (!result.IsSuccess)
                return Result<UserDto>.Fail
                    (ErrorMessages.OperationFailed(nameof(OperationNames.GetUserById),
                    result.Errors));
            return Result<UserDto>.Ok(result.Data);
        }
    }
}
