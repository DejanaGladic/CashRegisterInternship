namespace CashRegister.Domain.DTO
{
    public class BillDTO
    {
        public string BillNumber { get; set; }
        public string PaymentMethod { get; set; }
        public int TotalBillPrice { get; set; }
        public string CreditCardNumber { get; set; }
    }
}
