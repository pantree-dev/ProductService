using FluentValidation;
using ProductService.Domain.Enums;
using ProductService.Dtos;

namespace ProductService.Validators;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x=> x.ProductCategory).IsEnumName(typeof(ProductCategory), caseSensitive: false);
        RuleFor(x => x.Name).NotEmpty();
    }
}