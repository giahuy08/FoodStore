using System.ComponentModel.DataAnnotations;

namespace FoodStoreAPI.Entities
{
    public class Category:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
