using FoodStoreAPI.Repositories.Interfaces;

namespace FoodStoreAPI.Repositories
{
    public class CurrentTime : ICurrentTime
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow;
        }
    }
}
