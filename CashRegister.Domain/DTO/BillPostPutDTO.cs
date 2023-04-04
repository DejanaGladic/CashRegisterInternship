namespace CashRegister.Domain.DTO
{
    public class BillPostPutDTO
    {

        public string BillNumber { get; set; }
        public string PaymentMethod { get; set; }
        public string CreditCardNumber { get; set; }
        public BillPostPutDTO(string billNumber, string paymentMethod, string creditCardNumber)
        {
            BillNumber = billNumber;
            PaymentMethod = paymentMethod;
            CreditCardNumber = creditCardNumber;
        }
    }
}
