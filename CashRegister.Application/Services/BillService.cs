using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;

namespace CashRegister.Application.Services
{
    public class BillService : IBillService
    {
        private IUnitOfWork _unitOfWork;
        private ICalculator _calculator;
        public BillService(IUnitOfWork unitOfWork, ICalculator  calculator)
        {
            _unitOfWork = unitOfWork;
            _calculator = calculator;
        }
        public async Task<bool> CreateBill(Bill bill)
        {
            var ifBillExists = _unitOfWork.BillRepository.GetByStringId(bill.BillNumber);
            if (bill != null)
            {
                if (ifBillExists != null) {
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

        public async Task<bool> UpdateBill(Bill bill)
        {
            if (bill != null)
            {
                //preuzimanje direktne reference na objekat koji menjamo
                var returnedBill = _unitOfWork.BillRepository.GetByStringId(bill.BillNumber);

                if (returnedBill != null)
                {
                    returnedBill.BillNumber = bill.BillNumber;
                    returnedBill.PaymentMethod = bill.PaymentMethod;
                    returnedBill.TotalPrice = bill.TotalPrice;
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

        public async Task<bool> DeleteBill(string billNumber)
        {
            if (billNumber != null)
            {
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

        public async Task<IEnumerable<Bill>> GetAllBills()
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

        public void CalculateTotalBillPrice(ProductBill productBill, string typeOfCalculation) {

            var returnedProductBill = GetBillById(productBill.BillNumber);
            var initialValue = returnedProductBill.TotalPrice;
            var value = productBill.ProductsPrice;

            if(typeOfCalculation == "adds")
                productBill.Bill.TotalPrice = (int)_calculator.AdditionOperation(initialValue, value);
            else if(typeOfCalculation == "subtract")
                returnedProductBill.TotalPrice = (int)_calculator.SubstractOperation(initialValue, value);
        }


    }
}
