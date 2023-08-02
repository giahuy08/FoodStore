
using FoodStoreAPI.DbContext;
using FoodStoreAPI.Entities;
using FoodStoreAPI.Repositories;
using FoodStoreAPI.Repositories.Interfaces;


namespace FoodStore.Repositories
{
    public class CategoryRepository:GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext,
                                         ICurrentTime currentTime
                                      )
            : base(dbContext, currentTime)
        {

        }
    }
}
