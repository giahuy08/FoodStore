
using FoodStoreAPI.DbContext;
using FoodStoreAPI.Entities;
using FoodStoreAPI.Repositories.Interfaces;

namespace FoodStoreAPI.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext,
                                          ICurrentTime currentTime
                                       )
             : base(dbContext, currentTime)
        {

        }

    }
}
