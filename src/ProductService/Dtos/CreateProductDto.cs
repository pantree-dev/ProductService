using ProductService.Data.Entities;
using ProductService.Domain.Enums;

namespace ProductService.Dtos;

public class CreateProductDto
{
    public string Name { get; init; }
    public Guid? Id { get; init; }
    public Guid? Sku { get; init; }
    
    public String ProductCategory { get; init; }
    
    public Product MapToProduct()
    {
        return new Product
        {
            Id = Id ?? Guid.NewGuid(),
            Name = Name,
            Sku = Sku ?? Guid.NewGuid(),
            Active = true,
            ProductCategory = Enum.Parse<ProductCategory>(ProductCategory)
        };
    }
}