namespace CashRegister.Application.ServiceInterfaces
{
    public interface IValidationService
    {
        public bool IsValidBillNumber(string billNumber);
        public bool isValidCreditCard(string creditCard);
        public bool IsUpperLimitOverDrawn(int upperLimit);
    }
}
