namespace CashRegister.Domain.DTO
{
    public class ProductBillDTO
    {
        public string BillNumber { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductsPrice { get; set; }
    }
}
