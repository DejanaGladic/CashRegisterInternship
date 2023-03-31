using CashRegister.Application.DTO;
using CashRegister.Domain.DTO;
using FluentValidation;

namespace CashRegister.API.Validators
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator() {
            RuleFor(product => product.Price).NotEmpty().WithMessage("Please add a product price");
        }
    }
}
