using ProductService.Common.Exceptions;

namespace ProductService.Data.Exceptions;

public class ProductExistsException() : BadRequestException("Product already exists");