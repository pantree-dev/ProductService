using Microsoft.EntityFrameworkCore;
using ProductService.Data.Entities;

namespace ProductService.Data;

public class ProductDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ProductDbContext():base()
    {
        
    }
    public ProductDbContext(DbContextOptions<ProductDbContext> options): base(options)
    {
        
    }


}