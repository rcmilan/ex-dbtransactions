using ex_dbtransactions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ex_dbtransactions.UoW
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable where TContext : DbContext, new()
    {
        private readonly TContext _context;
        private string _errorMessage = string.Empty;
        private IDbContextTransaction _objTran;
        private Dictionary<string, object> _repositories;
        private bool disposedValue;

        //Using the Constructor we are initializing the _context variable is nothing but
        //we are storing the DBContext (ApplicationDBContext) object in _context variable
        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public TContext Context => _context;

        //If all the Transactions are completed successfuly then we need to call this Commit()
        //method to Save the changes permanently in the database
        public void Commit()
        {
            _objTran.Commit();
        }

        //This CreateTransaction() method will create a database Trnasaction so that we can do database operations by
        //applying do evrything and do nothing principle
        public void CreateTransaction()
        {
            _objTran = _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        //If atleast one of the Transaction is Failed then we need to call this Rollback()
        //method to Rollback the database changes to its previous state
        public void Rollback()
        {
            _objTran.Rollback();
            _objTran.Dispose();
        }

        //This Save() Method Implement DbContext Class SaveChanges method so whenever we do a transaction we need to
        //call this Save() method so that it will make the changes in the database
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                _errorMessage = $"db update error: {dbEx?.InnerException?.Message}";

                throw new Exception(_errorMessage, dbEx);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public IRepository<T, TId> GenericRepository<T, TId>() where T : BaseEntity<TId>
        {
            _repositories ??= new Dictionary<string, object>();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(IRepository<T, TId>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (IRepository<T, TId>)_repositories[type];
        }
    }
}