using Microsoft.EntityFrameworkCore;
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
        var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
        
        if(existingProduct != null)
        {
            throw new ProductExistsException();
        }
        
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }
    

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _dbContext.Products.ToListAsync();
    }
}