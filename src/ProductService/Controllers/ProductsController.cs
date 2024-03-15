using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductService.Data.Entities;
using ProductService.Data.Repositories.Interfaces;
using ProductService.Dtos;

namespace ProductService.Controllers;

[Route("api/[controller]")]
public class ProductsController(IProductRepository productRepository, IValidator<CreateProductDto> createProductValidator) : ControllerBase
{
    // GET api/products
    [HttpGet]
    public async Task<IEnumerable<Product>> Get()
    {
        return await productRepository.GetAllProducts();
    }
    
    // PUT api/products
    [HttpPut]
    public async Task<Product> Put([FromBody]CreateProductDto inputProduct)
    {

        ArgumentNullException.ThrowIfNull(inputProduct);
        await createProductValidator.ValidateAndThrowAsync(inputProduct);

        var newProduct = inputProduct.MapToProduct();
        return await productRepository.CreateProduct(newProduct);
    }
}