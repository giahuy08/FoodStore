using FoodStoreAPI.DbContext;
using FoodStoreAPI.Repositories.Interfaces;

namespace FoodStoreAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public IProductRepository ProductRepository { get; }

        public ICartRepository CartRepository { get; }

        public ICategoryRepository CategoryRepository { get; }

        public IPhotoRepository PhotoRepository { get; }

        public UnitOfWork(AppDbContext appDbContext, IProductRepository productRepository,
                          ICartRepository cartRepository, IPhotoRepository photoRepository,
                          ICategoryRepository categoryRepository)
        {
            _appDbContext = appDbContext;
            ProductRepository = productRepository;
            CartRepository = cartRepository;
            CategoryRepository = categoryRepository;
            PhotoRepository = photoRepository;
        }

        public void BeginTransaction()
        {
            _appDbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _appDbContext.Database.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _appDbContext.Database.RollbackTransaction();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
    }
}
