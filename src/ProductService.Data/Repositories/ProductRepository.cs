using Microsoft.EntityFrameworkCore;
using ProductService.Common.Exceptions;
using ProductService.Data.Entities;
using ProductService.Data.Exceptions;
using ProductService.Data.Repositories.Interfaces;

namespace ProductService.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _dbContext;

    public ProductRepository(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Product> CreateProduct(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }
    

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _dbContext.Products.Where(x => x.Active).ToListAsync();
    }

    public async Task<Product?> GetProductById(Guid id) => await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Product?> GetProductByName(string name) => await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == name);
    public async Task SetDisabled(Guid productId)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (product == null)
        {
            throw new NotFoundException();
        }

        if (product.Active == false)
        {
            return;
        }
        
        product.Active = false;
        product.DisabledDate = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
    }
}