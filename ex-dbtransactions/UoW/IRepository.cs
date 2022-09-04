using ex_dbtransactions.Entities;
using System.Linq.Expressions;

namespace ex_dbtransactions.UoW
{
    public interface IRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        TEntity Add(TEntity entity);

        void BulkInsert(IEnumerable<TEntity> entities);

        Task<int> CountAsync();

        TEntity Delete(TEntity entity);

        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null);

        void SetEntryModified(TEntity entity);

        TEntity Update(TEntity entity);
    }
}