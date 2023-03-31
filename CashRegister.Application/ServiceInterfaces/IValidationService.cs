namespace CashRegister.Application.ServiceInterfaces
{
    public interface IValidationService
    {
        public bool IsUpperLimitOverDrawn(int upperLimit);
    }
}
