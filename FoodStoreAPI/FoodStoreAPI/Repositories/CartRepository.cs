
using FoodStoreAPI.DbContext;
using FoodStoreAPI.Entities;
using FoodStoreAPI.Repositories;
using FoodStoreAPI.Repositories.Interfaces;


namespace FoodStore.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext dbContext,
                                          ICurrentTime currentTime
                                       )
             : base(dbContext, currentTime)
        {

        }

    }
}
