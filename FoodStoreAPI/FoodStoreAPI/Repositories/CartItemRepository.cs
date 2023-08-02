
using FoodStoreAPI.DbContext;
using FoodStoreAPI.Entities;
using FoodStoreAPI.Repositories;
using FoodStoreAPI.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FoodStore.Repositories
{
    public class CartItemRepository:GenericRepository<CartItem>,ICartItemRepository
    {
        public CartItemRepository(AppDbContext dbContext,
                                         ICurrentTime currentTime
                                      )
            : base(dbContext, currentTime)
        {

        }
    }
}
