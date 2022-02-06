using System.Linq.Expressions;

namespace Bank.Core.Persistence
{
    // todo: split readrepository and storerepository(create,update)
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// The GetByIdAsync.
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity?> GetByIdAsync<TId>(TId id);

        /// <summary>
        /// The GetListAsync.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);
    }
}
