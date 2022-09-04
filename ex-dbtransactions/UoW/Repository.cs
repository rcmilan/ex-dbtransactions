using ex_dbtransactions.Database;
using ex_dbtransactions.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ex_dbtransactions.UoW
{
    public class Repository<TEntity, TId> : IDisposable, IRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private readonly ApplicationDbContext Context;

        private bool disposedValue;

        private Repository(ApplicationDbContext context)
        {
            disposedValue = false;
            this.Context = context;
        }

        public Repository(IUnitOfWork<ApplicationDbContext> unitOfWork) : this(unitOfWork.Context)
        {
        }

        public virtual IQueryable<TEntity> Table
        {
            get { return Entities; }
        }

        private DbSet<TEntity> Entities => Context.Set<TEntity>();

        public TEntity Add(TEntity entity)
        {
            Entities.Add(entity);

            return entity;
        }

        public void BulkInsert(IEnumerable<TEntity> entities)
        {
            Context.ChangeTracker.AutoDetectChangesEnabled = false;
            Context.Set<TEntity>().AddRange(entities);
            Context.SaveChanges();
        }

        public Task<int> CountAsync() => Entities.CountAsync();

        public TEntity Delete(TEntity entity)
        {
            Entities.Remove(entity);

            return entity;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
            => Entities.FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                Entities.Where(predicate);

            return Entities.AsEnumerable();
        }

        public virtual void SetEntryModified(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual TEntity Update(TEntity entity)
        {
            SetEntryModified(entity);

            return entity;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }
    }
}