using Bookify_Library_mgnt.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookify_Library_mgnt.Configurations
{
    public static class DbContextConfiguration
    {
        public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
