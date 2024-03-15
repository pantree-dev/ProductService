using ProductService.Data.Entities;

namespace ProductService.Data.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Product> CreateProduct(Product product);
    Task<IEnumerable<Product>> GetAllProducts();
    
    Task<Product?> GetProductById(Guid id);
    Task<Product?> GetProductByName(string name);
    Task SetDisabled(Guid productId);
}