namespace FoodStoreAPI.Entities
{
    public class CartItem:BaseEntity
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
