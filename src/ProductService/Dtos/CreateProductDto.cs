using ProductService.Data.Entities;
using ProductService.Domain.Enums;

namespace ProductService.Dtos;

public class CreateProductDto
{
    public string Name { get; init; }
    
    public String ProductCategory { get; init; }
    
    public Product MapToProduct()
    {
        return new Product
        {
            Name = Name,
            Active = true,
            ProductCategory = Enum.Parse<ProductCategory>(ProductCategory)
        };
    }
}