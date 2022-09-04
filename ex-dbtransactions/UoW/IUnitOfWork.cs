using Microsoft.EntityFrameworkCore;

namespace ex_dbtransactions.UoW
{
    public interface IUnitOfWork<out TContext> where TContext : DbContext, new()
    {
        TContext Context { get; }

        void Commit();

        void CreateTransaction();

        void Rollback();

        void Save();
    }
}