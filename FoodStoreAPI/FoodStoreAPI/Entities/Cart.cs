using FoodStoreAPI.Entities.Identity;

namespace FoodStoreAPI.Entities
{
    public class Cart:BaseEntity
    {
        public string UserId { get; set; } = String.Empty;
        public virtual User? User { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
