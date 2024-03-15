using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductService.Data.Entities;
using ProductService.Data.Repositories.Interfaces;
using ProductService.Dtos;

namespace ProductService.Controllers;

[Route("api/[controller]")]
public class ProductsController
{
    private readonly IProductRepository _productRepository;
    private readonly IValidator<CreateProductDto> _createProductValidator;

    public ProductsController(IProductRepository productRepository, IValidator<CreateProductDto> createProductValidator):base()
    {
        _productRepository = productRepository;
        _createProductValidator = createProductValidator;
    }
    // GET api/products
    [HttpGet]
    public async Task<IEnumerable<Product>> Get()
    {
        return await _productRepository.GetAllProducts();
    }
    
    // PUT api/products
    [HttpPut]
    public async Task<ActionResult<Product>> Put([FromBody]CreateProductDto inputProduct)
    {

        ArgumentNullException.ThrowIfNull(inputProduct);
        await _createProductValidator.ValidateAndThrowAsync(inputProduct);

        var existingProduct = await _productRepository.GetProductByName(inputProduct.Name);

        if (existingProduct != null)
        {
            return new BadRequestResult();
        }

        var newProduct = inputProduct.MapToProduct();
        return await _productRepository.CreateProduct(newProduct);
    }

    [HttpPut("{productId}/disable")]
    public async Task Disable(Guid productId)
    {
        await _productRepository.SetDisabled(productId);
    }
}