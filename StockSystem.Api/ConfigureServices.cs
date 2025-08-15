using StockSystem.Api.Data;
using StockSystem.Api.Services;
using StockSystem.Api.Services.Interface;

namespace StockSystem.Api
{
        public static class ConfigureServices
        {
                public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
                {
                        services.AddHttpClient();
                        services.AddHttpContextAccessor(); 

                        services.AddScoped<UnitOfWork>();

                        services.AddScoped<IProductService, ProductService>();
                        services.AddScoped<IStockService, StockService>();

                        services.AddScoped<IShoppingCartService, ShoppingCartService>();

                        return services;
                }
        }
}