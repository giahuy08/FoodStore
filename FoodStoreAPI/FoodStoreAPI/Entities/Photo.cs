namespace FoodStoreAPI.Entities
{
    public class Photo : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
