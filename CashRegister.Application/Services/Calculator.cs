using CashRegister.Application.ServiceInterfaces;

namespace CashRegister.Application.Services
{
    public class Calculator : ICalculator
    {
        public decimal AdditionOperation(decimal initialValue, decimal valueToAdd)
        {
           return initialValue + valueToAdd;
        }

        public int moneyConversion(int price, string exChangeRate)
        {
            switch (exChangeRate)
            {
                case "EUR":
                    return price / 118;
                case "USD":
                    return price / 107;
                default:
                    return price;
            }
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
