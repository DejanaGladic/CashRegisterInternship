using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.ServiceInterfaces
{
    public interface IValidationService
    {
        public bool IsValidBillNumber(string billNumber);
        public bool isValidCreditCard(string creditCard);
        public bool IsUpperLimitOverDrawn(int upperLimit);
    }
}
