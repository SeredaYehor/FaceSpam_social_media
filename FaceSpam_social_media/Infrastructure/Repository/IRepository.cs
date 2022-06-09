using System.Linq;
using System.Threading.Tasks;
using FaceSpam_social_media.Infrastructure.Data;
using System.Collections.Generic;

namespace FaceSpam_social_media.Infrastructure.Repository
{
    public interface IRepository
    {
        /// <summary>
        ///     Adds new entity
        /// </summary>
        /// <param name="entity">Entity that should be created.</param>
        /// <returns>Returns new created entity.</returns>
        Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        ///     Updates entity
        /// </summary>
        /// <returns>Returns updated entity.</returns>
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        ///     Returns queryable collection with all entities with active status
        /// </summary>
        /// <typeparam name="TEntity">Current entity type</typeparam>
        /// <returns></returns>
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity;

        Task<TEntity> DeleteAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;

        Task<int> DeleteAsyncRange<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, IEntity;
    }
}