using DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace DAO.Databases
{
    internal class AccountingDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AccountingDbContext()
            : base()
        {
        }

        public AccountingDbContext(string connectionString)
            : base(new DbContextOptionsBuilder()
                  .UseSqlServer(connectionString)
                  .Options)
        {
        }

        public AccountingDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=accountingapp;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .Property(u => u.Id)
                .HasDefaultValueSql("newid()");
        }
    }
}
