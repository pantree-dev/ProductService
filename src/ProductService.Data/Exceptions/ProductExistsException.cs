using ProductService.Common.Exceptions;

namespace ProductService.Data.Exceptions;


public class ProductExistsException : BadRequestException
{
    public ProductExistsException() : base("Product already exists")
    {
    }
}