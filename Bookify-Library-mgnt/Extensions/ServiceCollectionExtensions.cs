using Bookify_Library_mgnt.Repositpries.Implementations;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Implementations;
using Bookify_Library_mgnt.Services.Interfaces;

namespace Bookify_Library_mgnt.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //repositories
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IBorrowingRepository, BorrowingRepository>();
            //services
            services.AddScoped<IReviewService, ReviewService>();

            return services;
        }
    }
}
