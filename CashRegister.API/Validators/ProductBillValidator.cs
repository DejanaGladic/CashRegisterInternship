using CashRegister.Domain.DTO;
using FluentValidation;

namespace CashRegister.API.Validators
{
    public class ProductBillValidator : AbstractValidator<ProductBillPostPutDTO>
    {
        public ProductBillValidator() {
            RuleFor(productBill => productBill.ProductQuantity).NotEmpty().WithMessage("Please add a product quantity");
        }

    }
}
