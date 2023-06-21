using Application.Contracts;
using Application.Contracts.ProuductServices;
using Infrastructure.DB;
using Infrastructure.Services.ProductServices;
using Infrastructure.Services.ShoppingCartServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnectionString = configuration.GetConnectionString("ToTShop");
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(defaultConnectionString));
            services.AddScoped<IShoppingCartServices, ShoppingCartServices>();
            services.AddScoped<IProductServices, ProductServices>();

            return services;
        }
    }
}
