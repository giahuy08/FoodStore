using FoodStoreAPI.Commons.Enums;
using FoodStoreAPI.Entities.Identity;

namespace FoodStoreAPI.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public virtual User? User { get; set; }
        public Address AddressBook { get; set; }
        public decimal ShippingCash { get; set; }
        public decimal SubtotalPrice { get; set; }
        public OrderStatusEnum Status { get; set; }
    }
}
