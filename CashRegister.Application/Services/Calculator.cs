using CashRegister.Application.ServiceInterfaces;

namespace CashRegister.Application.Services
{
    public class Calculator : ICalculator
    {
        public decimal AdditionOperation(decimal initialValue, decimal valueToAdd)
        {
           return initialValue + valueToAdd;
        }

        public decimal MultiplyOperation(decimal initialValue, decimal multiplyWith) {
            return initialValue * multiplyWith;
        }

        public decimal SubstractOperation(int initialValue, decimal valueToSubstract)
        {
            return initialValue - valueToSubstract;
        }
    }
}
