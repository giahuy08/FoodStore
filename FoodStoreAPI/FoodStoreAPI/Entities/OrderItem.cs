using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStoreAPI.Entities
{
    public class OrderItem:BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }  

        public decimal TotalPrice
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }
    }
}
