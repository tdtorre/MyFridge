using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MyFridge.Data
{
    public static class Configure
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MyFridgeContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("MyFridge.Data")));
        }
    }
}