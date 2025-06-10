using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Data.Helpers;

public class UnitOfWork(
    ApplicationDbContext _context,
    IAuthRepository _authRepository,
    IBookRepository _bookRepository,
    IBorrowingRepository _borrowingRepository,
    ICategoryRepository _categoryRepository,
    IReviewRepository _reviewRepository,
    UserManager<User> _userManager,
    IPasswordHasher<User> _passwordHasher,
    RoleManager<IdentityRole> _roleManager,
    IClaimRepository _claimRepository,
    IRoleRepository _roleRepository) : IUnitOfWork, IDisposable
{
    public IAuthRepository AuthRepository =>
        _authRepository ??= new AuthRepository(_context, _userManager, _passwordHasher);
    public IBookRepository BookRepository =>
        _bookRepository ??= new BookRepository(_context);
    public ICategoryRepository CategoryRepository =>
        _categoryRepository ??= new CategoryRepository(_context);
    public IReviewRepository ReviewRepository =>
        _reviewRepository ??= new ReviewRepository(_context);
    public IBorrowingRepository BorrowingRepository =>
        _borrowingRepository ??= new BorrowingRepository(_context);
    public IClaimRepository ClaimRepository =>
        _claimRepository ??= new ClaimRepository(_userManager);
    public IRoleRepository RoleRepository =>
        _roleRepository ??= new RoleRepository(_userManager, _roleManager);

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
