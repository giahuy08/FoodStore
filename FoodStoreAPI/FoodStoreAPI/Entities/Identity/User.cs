using FoodStoreAPI.Commons.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStoreAPI.Entities.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public override string? UserName { get => base.UserName; set => base.UserName = value; }
        //public byte[]? PasswordHash { get; set; }
        //public byte[] PasswordSalt { get; set; }
        public GenderEnum Gender { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        [NotMapped]
        public DateOnly Birthday { get; set; }
        public override string Email { get; set; }
        public string? UrlAvatar { get; set; }
        public RoleEnum Role { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual Cart? Cart { get; set; }
    }
}
