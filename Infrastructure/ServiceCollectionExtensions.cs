using Application.Authorization.Dtos.Roles;
using Application.Authorization.Services;
using Application.Authorization.Validators;
using Application.Books.Commands;
using Application.Books.Dtos;
using Application.Books.Services;
using Application.Books.Validators;
using Application.Borrowings.Dtos;
using Application.Borrowings.Services;
using Application.Borrowings.Validators;
using Application.Categories.Dtos;
using Application.Categories.Services;
using Application.Categories.Validators;
using Application.Reviews.Dtos;
using Application.Reviews.Services;
using Application.Reviews.Validators;
using Application.Users.Dtos;
using Application.Users.Services;
using Application.Users.Validators;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositpries;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
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
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

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
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            //user
            services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
            services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
            //role
            services.AddScoped<IValidator<CreateRoleDto>, CreateRoleDtoValidator>();
            services.AddScoped<IValidator<UpdateRoleDto>, UpdateRoleDtoValidator>();
            //book
            services.AddScoped<IValidator<CreateBookDto>, CreateBookDtoValidator>();
            services.AddScoped<IValidator<UpdateBookDto>, UpdateBookDtoValidator>();
            //borrowing
            services.AddScoped<IValidator<CreateBorrowingDto>, CreateBorrowingDtoValidator>();
            services.AddScoped<IValidator<UpdateBorrowingDto>, UpdateBorrowingDtoValidator>();
            //category
            services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryDtoValidator>();
            services.AddScoped<IValidator<UpdateCategoryDto>, UpdateCategoryDtoValidator>();
            //Review
            services.AddScoped<IValidator<CreateReviewDto>, CreateReviewDtoValidator>();
            services.AddScoped<IValidator<UpdateReviewDto>, UpdateReviewDtoValidator>();
            //login
            services.AddScoped<IValidator<LoginDto>, LoginDtoValidation>();

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
            services.AddValidators();
            services.AddMediator();
            return services;
        }
    }
}
