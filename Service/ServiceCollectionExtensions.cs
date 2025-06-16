using Application.Authorization.Services;
using Application.Books.Commands;
using Application.Books.Services;
using Application.Borrowings.Services;
using Application.Categories.Services;
using Application.Reviews.Services;
using Application.Users.Services;
using Data.Entities;
using Data.Helpers;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;

namespace Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBookServices(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookService>();
        return services;
    }
    public static IServiceCollection AddRoleServices(this IServiceCollection services)
    {
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }

    public static IServiceCollection AddCategoryServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }

    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }
    public static IServiceCollection AddTokenServices(this IServiceCollection services)
    {
        services.AddTransient<ITokenService, TokenService>();

        return services;
    }
    public static IServiceCollection AddClaimServices(this IServiceCollection services)
    {
        services.AddScoped<IClaimService, ClaimService>();
        services.AddScoped<IClaimRepository, ClaimRepository>();

        return services;
    }
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddReviewServices(this IServiceCollection services)
    {
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IReviewService, ReviewService>();

        return services;
    }

    public static IServiceCollection AddBorrowingServices(this IServiceCollection services)
    {
        services.AddScoped<IBorrowingRepository, BorrowingRepository>();
        services.AddScoped<IBorrowingService, BorrowingService>();

        return services;
    }
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(configurations =>
        {
            configurations.RegisterServicesFromAssembly(typeof(CreateBookCommand).Assembly);
        });
        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddBookServices();
        services.AddCategoryServices();
        services.AddAuthServices();
        services.AddReviewServices();
        services.AddBorrowingServices();
        services.AddRoleServices();
        services.AddMediator();
        services.AddCommonServices();
        services.AddClaimServices();
        services.AddTokenServices();
        return services;
    }
}
