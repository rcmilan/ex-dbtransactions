using ex_dbtransactions.Entities;
using Microsoft.EntityFrameworkCore;

namespace ex_dbtransactions.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(p => p.Id);

            modelBuilder.Entity<Person>().OwnsOne(p => p.Address, pa =>
            {
                pa.ToTable("Addresses");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}