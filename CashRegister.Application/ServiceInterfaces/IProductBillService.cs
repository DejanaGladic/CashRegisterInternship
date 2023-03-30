﻿using CashRegister.Domain.Models;

namespace CashRegister.Application.ServiceInterfaces
{
    public interface IProductBillService
    {
        Task<bool> CreateProductBill(ProductBill _productBill);
        Task<bool> UpdateProductBill(ProductBill _productBill);

        Task<bool> DeleteProductBill(string billNumber, int productId);
    }
}