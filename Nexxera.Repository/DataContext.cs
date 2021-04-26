using Microsoft.EntityFrameworkCore;
using Nexxera.Domain;

namespace Nexxera.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options){}

        public DbSet<Account> Accounts{get;set;}
        public DbSet<CreditCard> CreditCards{get;set;}
        public DbSet<CreditCardHistory> CreditCardHistories{get;set;}
        public DbSet<Customer> Customers{get;set;}
        public DbSet<DebtHistory> DebtHistories{get;set;}
        public DbSet<Period> Periods{get;set;}

    }
}