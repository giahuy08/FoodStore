using FoodStoreAPI.Entities;

namespace FoodStoreAPI.Resources.Responses
{
    public class UserResponse
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public virtual Address? Address { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        //public DateOnly Birthday { get; set; }
        public string? Gender { get; set; }
        public Cart? Cart { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
