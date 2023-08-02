using FoodStoreAPI.DbContext;
using FoodStoreAPI.Entities;
using FoodStoreAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FoodStoreAPI.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbSet<TEntity> _dbSet;
        private readonly ICurrentTime _currentService;
        public GenericRepository(AppDbContext dbContext, ICurrentTime currentService)
        {
            _dbSet = dbContext.Set<TEntity>();
            _currentService = currentService;
        }

        public async Task AddAsync(TEntity entity)
        {
            entity.CreationDate = _currentService.GetCurrentTime();
            //entity.CreatedBy = _claimsServices.GetCurrentUserId;
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreationDate = _currentService.GetCurrentTime();
            }
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> ExistAnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void SoftRemove(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletionDate = _currentService.GetCurrentTime();
            _dbSet.Update(entity);
        }

        public void SoftRemoveRange(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeletionDate = _currentService.GetCurrentTime();
            }
            _dbSet.UpdateRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(ICollection<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IQueryable<TEntity> All()
        {
            return _dbSet
                .AsQueryable();
        }
        public void Update(TEntity entity)
        {
            entity.ModificationDate = _currentService.GetCurrentTime();
            //entity.ModificationBy = _claimsServices.GetCurrentUserId;
            _dbSet.Update(entity);
        }

        public void UpdateRange(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreationDate = _currentService.GetCurrentTime();
            }
            _dbSet.UpdateRange(entities);
        }


        public async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
