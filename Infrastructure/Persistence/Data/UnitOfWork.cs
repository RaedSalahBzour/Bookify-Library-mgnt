using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositpries;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        private IAuthRepository _authRepository;
        private IBookRepository _bookRepository;
        private IBorrowingRepository _borrowingRepository;
        private ICategoryRepository _categoryRepository;
        private IReviewRepository _reviewRepository;
        private ITokenRepository _tokenRepository;
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
            ITokenRepository tokenRepository,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _authRepository = authRepository;
            _bookRepository = bookRepository;
            _borrowingRepository = borrowingRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _tokenRepository = tokenRepository;
            _roleManager = roleManager;
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
        public ITokenRepository TokenRepository =>
            _tokenRepository ??= new TokenRepository(_userManager, _roleManager);

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
