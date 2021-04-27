using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nexxera.Domain;

namespace Nexxera.Repository
{
    public class NexxeraRepository : INexxeraRepository
    {
        private readonly DataContext _context;
        public  NexxeraRepository(DataContext context){
            _context = context;
            // remove tracking to not lock resource in the entity framework
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
             _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            // if save return  true
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Account> GetAccount(int customerId)
        {
            IQueryable<Account> query = _context.Accounts.
            Include(e => e.CreditCards).
            Include(e => e.DebtHistories);

            query = query
                        .AsNoTracking()
                        .OrderBy(c => c.Id)
                        .Where(c => c.Id == customerId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Customer[]> GetAllCustomer()
        {
            IQueryable<Customer> query = _context.Customers;
            return await query.ToArrayAsync();
        }

        public async Task<Period[]> GetAllPeriod()
        {
            IQueryable<Period> query = _context.Periods;
            return await query.ToArrayAsync();
        }

        public async Task<CreditCard> GetCreditCard(int accountId, int periodId)
        {
            IQueryable<CreditCard> query = _context.CreditCards.            
            Include(e => e.CreditCardHistories).Include( e => e.CreditCardHistories.Select( c => c.PeriodId == periodId));

            query = query
                        .AsNoTracking()
                        .OrderBy(c => c.Id)
                        .Where(c => c.AccountId == accountId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<DebtHistory> GetDebtHistory(int accountId, int periodId)
        {
            IQueryable<DebtHistory> query = _context.DebtHistories.
            Include(e => e.Period);
            

            query = query
                        .AsNoTracking()
                        .OrderBy(c => c.Id)
                        .Where(c => c.AccountId == accountId && c.PeriodId == periodId );
            return await query.FirstOrDefaultAsync();
        }

        public void Update<T>(T entity) where T : class
        {
           _context.Update(entity);
        }

    }
}