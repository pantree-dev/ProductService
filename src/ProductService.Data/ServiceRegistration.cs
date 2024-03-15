using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Data.Entities;
using ProductService.Data.Repositories;
using ProductService.Data.Repositories.Interfaces;

namespace ProductService.Data;

public static class ServiceRegistration
{

    public static IServiceCollection RegisterDataServices(this IServiceCollection collection, string connectionString)
    {
         collection.AddDbContext<ProductDbContext>(options =>
         {
             options.UseNpgsql(connectionString, b => b.MigrationsAssembly("ProductService"));
         });

         collection.AddScoped<IProductRepository, ProductRepository>();
        return collection;
    }
}