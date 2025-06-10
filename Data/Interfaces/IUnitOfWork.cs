using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces;

public interface IUnitOfWork
{
    IAuthRepository AuthRepository { get; }
    IBookRepository BookRepository { get; }
    IBorrowingRepository BorrowingRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IReviewRepository ReviewRepository { get; }
    IClaimRepository ClaimRepository { get; }
    IRoleRepository RoleRepository { get; }
    Task CompleteAsync();

}
