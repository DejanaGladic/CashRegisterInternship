namespace CashRegister.Application.ServiceInterfaces
{
    public interface ICalculator
    {
        public decimal AdditionOperation(decimal initialValue, decimal valueToAdd);
        public decimal MultiplyOperation(decimal initialValue, decimal multiplyWith);
        public decimal SubstractOperation(int initialValue, decimal valueToSubstract);
    }
}
