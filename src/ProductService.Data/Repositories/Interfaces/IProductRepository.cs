using ProductService.Data.Entities;

namespace ProductService.Data.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Product> CreateProduct(Product product);
    Task<IEnumerable<Product>> GetAllProducts();
}