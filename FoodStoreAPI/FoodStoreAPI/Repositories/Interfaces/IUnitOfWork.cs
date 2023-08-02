
namespace FoodStoreAPI.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public ICartRepository CartRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IPhotoRepository PhotoRepository { get; }

        Task<int> SaveChangesAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
    }
}
