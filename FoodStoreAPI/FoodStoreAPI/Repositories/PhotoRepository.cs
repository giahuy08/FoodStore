
using FoodStoreAPI.DbContext;
using FoodStoreAPI.Entities;
using FoodStoreAPI.Repositories.Interfaces;

namespace FoodStoreAPI.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(AppDbContext dbContext,
                                          ICurrentTime currentTime
                                       )
             : base(dbContext, currentTime)
        {

        }
    }
}
