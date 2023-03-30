namespace CashRegister.Domain.DTO
{
    public class BillPostPutDTO
    {
        public string BillNumber { get; set; }
        public string PaymentMethod { get; set; }
        public string CreditCardNumber { get; set; }
    }
}
