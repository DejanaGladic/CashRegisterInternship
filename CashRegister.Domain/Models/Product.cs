using System.ComponentModel.DataAnnotations;


namespace CashRegister.Domain.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public List<ProductBill> ProductBills { get; set; }
    }
}
