using FoodStoreAPI.Entities;
using System.Linq.Expressions;

namespace FoodStoreAPI.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        IQueryable<TEntity> All();
        Task AddRangeAsync(ICollection<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(ICollection<TEntity> entities);
        void SoftRemove(TEntity entity);
        void SoftRemoveRange(ICollection<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(ICollection<TEntity> entities);
        Task<bool> ExistAnyAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

    }
}
