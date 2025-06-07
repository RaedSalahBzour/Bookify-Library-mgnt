using Application.Users.Dtos;
using Application.Users.Queries;
using Application.Users.Services;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Handlers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedResult<UserDto>>
    {
        private readonly IAuthService _authService;

        public GetUsersQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<PagedResult<UserDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var result = await _authService.GetUsersAsync(query.pageNumber, query.pageSize);
            return new PagedResult<UserDto>
            {
                PageNumber = query.pageNumber,
                PageSize = query.pageSize,
                Items = result.Items,
                TotalCount = result.TotalCount,
            };
        }
    }
}
