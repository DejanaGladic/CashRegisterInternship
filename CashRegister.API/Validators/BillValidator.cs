using CashRegister.Domain.DTO;
using FluentValidation;

namespace CashRegister.API.Validators
{
    public class BillValidator : AbstractValidator<BillPostPutDTO>
    {
        public BillValidator() {
            RuleFor(bill => bill.PaymentMethod).Must(IsValidPaymentMethod).WithMessage("A payment method is not valid");
            RuleFor(bill => bill.PaymentMethod).NotEmpty().WithMessage("Please add a payment method");
            RuleFor(bill => bill.BillNumber).Matches(@"^\d{3}-\d{13}-\d{2}$");
            RuleFor(bill => bill.BillNumber).Must(IsValidBillNumber).WithMessage("BillNumber is not valid");
            RuleFor(bill => bill.CreditCardNumber).CreditCard().WithMessage("Credit card number is not valid");
        }

        public bool IsValidPaymentMethod(string paymentMethod)
        {
            if (paymentMethod == "card" || paymentMethod == "cash")
            {
                return true;
            }
            return false;
        }

        public bool IsValidBillNumber(string billNumber)
        {
            string identificationCode = billNumber.Substring(0, 3);
            string billNum = billNumber.Substring(4, 13);
            string controlNum = billNumber.Substring(18, 2);
            string concatenatedNum = identificationCode + billNum;
            long multipliedNum = long.Parse(concatenatedNum) * 100;
            int controlCalc = 98 - (int)(multipliedNum % 97);
            int controlNumInt = int.Parse(controlNum);
            return controlCalc == controlNumInt;
        }
    }
}
