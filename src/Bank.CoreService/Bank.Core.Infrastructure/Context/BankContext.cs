using Bank.Core.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.Infrastructure.Context
{
    public  class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Account> Account { get; set; }

        public DbSet<Customer> Customer { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account").HasKey(t => t.Id);
            modelBuilder.Entity<Account>().Property(t => t.Id).ValueGeneratedNever();
            //modelBuilder.Entity<Account>().Property(t => t.CustomerId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Account>().Property(t => t.IBAN).HasMaxLength(32);
            modelBuilder.Entity<Account>().Property(t => t.Name).HasMaxLength(50);
            modelBuilder.Entity<Account>().Property(t => t.Currency).HasConversion<string>();
            modelBuilder.Entity<Account>().Property(t => t.Balance).HasPrecision(10,2).IsConcurrencyToken();
            modelBuilder.Entity<Account>().OwnsOne(t => t.AuditInfo);

            modelBuilder.Entity<Customer>().ToTable("Customer").HasKey(t => t.Id);
            modelBuilder.Entity<Customer>().Property(t => t.IdentityNumber).HasMaxLength(11);
            modelBuilder.Entity<Customer>().HasIndex(t => t.IdentityNumber).IsUnique();
            modelBuilder.Entity<Customer>().Property(t => t.FirstName).HasMaxLength(200);
            modelBuilder.Entity<Customer>().Property(t => t.FamilyName).HasMaxLength(250);
            modelBuilder.Entity<Customer>().OwnsOne(t => t.AuditInfo);
        }
    }
}
