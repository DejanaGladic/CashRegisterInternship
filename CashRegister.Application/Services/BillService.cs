﻿using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;

namespace CashRegister.Application.Services
{
    public class BillService : IBillService
    {
        public IUnitOfWork _unitOfWork;
        public BillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateBill(Bill bill)
        {
            var ifBillExists = await _unitOfWork.BillRepository.GetByStringId(bill.BillNumber);
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
                var returnedBill = await _unitOfWork.BillRepository.GetByStringId(bill.BillNumber);

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
    }
}
