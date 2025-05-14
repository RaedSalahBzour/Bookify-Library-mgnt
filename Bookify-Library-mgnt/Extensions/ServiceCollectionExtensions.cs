using Bookify_Library_mgnt.Repositpries.Implementations;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Implementations;
using Bookify_Library_mgnt.Services.Interfaces;

namespace Bookify_Library_mgnt.Extensions
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


        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddBookServices();
            services.AddCategoryServices();
            services.AddAuthServices();
            services.AddReviewServices();
            services.AddBorrowingServices();
            services.AddRoleServices();

            return services;
        }
    }
}
