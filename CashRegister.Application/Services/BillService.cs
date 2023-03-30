using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;

namespace CashRegister.Application.Services
{
    public class BillService : IBillService
    {
        private IUnitOfWork _unitOfWork;
        private ICalculator _calculator;
        private IValidationService _validationService;
        public BillService(IUnitOfWork unitOfWork, ICalculator  calculator, IValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _calculator = calculator;
            _validationService = validationService;
        }
        public async Task<bool> CreateBill(Bill bill)
        {
            var ifBillExists = _unitOfWork.BillRepository.GetByStringId(bill.BillNumber);
            if (bill != null)
            {
                if (ifBillExists != null) {
                    return false;
                }

                if(!_validationService.IsValidBillNumber(bill.BillNumber) || 
                    !_validationService.isValidCreditCard(bill.CreditCardNumber)){               
                    return false;
                }

                await _unitOfWork.BillRepository.Add(bill);

                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public bool UpdateBill(Bill bill)
        {
            if (bill != null)
            {
                //preuzimanje direktne reference na objekat koji menjamo
                var returnedBill = _unitOfWork.BillRepository.GetByStringId(bill.BillNumber);

                if (returnedBill != null)
                {
                    if (!_validationService.IsValidBillNumber(bill.BillNumber) ||
                        !_validationService.isValidCreditCard(bill.CreditCardNumber))
                    {
                        return false;
                    }
                    returnedBill.BillNumber = bill.BillNumber;
                    returnedBill.PaymentMethod = bill.PaymentMethod;
                    returnedBill.CreditCardNumber = bill.CreditCardNumber;

                    _unitOfWork.BillRepository.Update(returnedBill);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public bool DeleteBill(string billNumber)
        {
            if (billNumber != null)
            {
                if (!_validationService.IsValidBillNumber(billNumber))
                {
                    return false;
                }

                var bill = _unitOfWork.BillRepository.GetByStringId(billNumber);
                if (bill != null)
                {
                    _unitOfWork.BillRepository.Delete(bill);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<List<Bill>> GetAllBills()
        {
            var billLists = await _unitOfWork.BillRepository.GetAll();
            return billLists;
        }

        public Bill GetBillById(string billNumber)
        {
            if (billNumber != null)
            {
                var bill = _unitOfWork.BillRepository.GetByStringId(billNumber);
                if (bill != null)
                {
                    return bill;
                }
            }
            return null;
        }

        public bool IfBillByIdExists(string billNumber)
        {
            if (billNumber != null)
            {
                var result = _unitOfWork.BillRepository.IfExistsByStringId(billNumber);
                return result;
            }
            return false;
        }

        public int CalculateTotalBillPrice(ProductBill productBill, string typeOfCalculation) {

            var returnedBill = GetBillById(productBill.BillNumber);
            var initialValue = returnedBill.TotalPrice;
            var value = productBill.ProductsPrice;

            int calculatedTotalPrice = 0;
            if(typeOfCalculation == "adds")
                calculatedTotalPrice = (int)_calculator.AdditionOperation(initialValue, value);
            else if(typeOfCalculation == "subtract")
                calculatedTotalPrice = (int)_calculator.SubstractOperation(initialValue, value);

            if (!_validationService.IsUpperLimitOverDrawn(calculatedTotalPrice)) {
                returnedBill.TotalPrice = calculatedTotalPrice;
            }

            return returnedBill.TotalPrice;
        }

        public Bill GetBillExchangeRate(string billNumber, string exchangeRate)
        {
            var returnedProductBill = GetBillById(billNumber);
            if (returnedProductBill == null)
                return null;

            var value = _calculator.moneyConversion(returnedProductBill.TotalPrice, exchangeRate);
            returnedProductBill.TotalPrice = value;
            return returnedProductBill;
        }
    }
}
