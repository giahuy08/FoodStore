
using FoodStoreAPI.DbContext;
using FoodStoreAPI.Entities;
using FoodStoreAPI.Repositories;
using FoodStoreAPI.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FoodStore.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext dbContext,
                                          ICurrentTime currentTime
                                       )
             : base(dbContext, currentTime)
        {

        }
        
    }
}
