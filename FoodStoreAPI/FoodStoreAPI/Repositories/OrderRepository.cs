using FoodStoreAPI.DbContext;
using FoodStoreAPI.Entities;
using FoodStoreAPI.Repositories;
using FoodStoreAPI.Repositories.Interfaces;

namespace FoodStore.Repositories
{
    public class OrderRepository:GenericRepository<Order>,IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext,
                                          ICurrentTime currentTime
                                       )
             : base(dbContext, currentTime)
        {

        }
    }
}
