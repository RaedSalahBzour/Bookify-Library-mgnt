using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Data.Helpers;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;

    private IAuthRepository _authRepository;
    private IBookRepository _bookRepository;
    private IBorrowingRepository _borrowingRepository;
    private ICategoryRepository _categoryRepository;
    private IReviewRepository _reviewRepository;
    private IClaimRepository _claimRepository;
    private IRoleRepository _roleRepository;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UnitOfWork(ApplicationDbContext context,
        IAuthRepository authRepository,
        IBookRepository bookRepository,
        IBorrowingRepository borrowingRepository,
        ICategoryRepository categoryRepository,
        IReviewRepository reviewRepository,
        UserManager<User> userManager,
        IPasswordHasher<User> passwordHasher,
        RoleManager<IdentityRole> roleManager,
        IClaimRepository claimRepository,
        IRoleRepository roleRepository)
    {
        _context = context;
        _authRepository = authRepository;
        _bookRepository = bookRepository;
        _borrowingRepository = borrowingRepository;
        _categoryRepository = categoryRepository;
        _reviewRepository = reviewRepository;
        _userManager = userManager;
        _passwordHasher = passwordHasher;
        _roleManager = roleManager;
        _claimRepository = claimRepository;
        _roleRepository = roleRepository;
    }

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
