using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.Domain.Models
{
    public class ProductBill
    {
        public int ProductQuantity { get; set; }
        public int ProductsPrice { get; set; }

        [ForeignKey("Bill")]
        public string BillNumber { get; set; }
        public Bill Bill { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
