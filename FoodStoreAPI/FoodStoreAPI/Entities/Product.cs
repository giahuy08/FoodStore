using System.Drawing.Drawing2D;

namespace FoodStoreAPI.Entities
{

    public class Product :BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string NormalizedName { set; get; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public int Price { get; set; }
        public int SalePrice { get; set; }
    }
}
