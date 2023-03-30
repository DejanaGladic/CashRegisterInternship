namespace CashRegister.Domain.DTO
{
    public class ProductBillPostPutDTO
    {
        public string BillNumber { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
