using FoodStoreAPI.DbContext;
using FoodStoreAPI.Entities;
using FoodStoreAPI.Repositories;
using FoodStoreAPI.Repositories.Interfaces;

namespace FoodStore.Repositories
{
    public class AddressRepository:GenericRepository<Address>,IAddressRepository
    {
        public AddressRepository(AppDbContext dbContext,
                                         ICurrentTime currentTime
                                      )
            : base(dbContext, currentTime)
        {

        }
    }
}
