using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodStoreAPI.Entities.Identity;

namespace FoodStoreAPI.Entities
{
    public class Address:BaseEntity
    {
        [MaxLength(50)]
        public string District { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Province { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Ward { get; set; }
        [MaxLength(100)]
        public string DetailAddress { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public string? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
