using System.Linq;
using System.Threading.Tasks;
using FaceSpam_social_media.Infrastructure.Data;
using System.Collections.Generic;

namespace FaceSpam_social_media.Infrastructure.Repository
{
    public class EntityFrameworkRepository : IRepository
    {
        private readonly MVCDBContext _dbContext;

        public EntityFrameworkRepository(MVCDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var result = await _dbContext.Set<TEntity>()
                .AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var result = _dbContext.Set<TEntity>()
                .Update(entity);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }
        
        public async Task<TEntity> DeleteAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var result = _dbContext.Set<TEntity>()
                .Remove(entity);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity
        {
            return _dbContext.Set<TEntity>()
                .Select(i => i);
        }

        public async Task<int> DeleteAsyncRange<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, IEntity
        {
            _dbContext.Set<TEntity>().RemoveRange(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }
    }
}