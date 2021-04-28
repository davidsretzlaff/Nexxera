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

        public async Task<Account> GetAccount(int? customerId, int? accountId)
        {
            IQueryable<Account> query = _context.Accounts
            .Include(e => e.Customer)
            .Include(e => e.CreditCards)
            .Include(e => e.DebtHistories);

            query = query
                        .AsNoTracking()
                        .OrderBy(c => c.Id);
                        
            if(customerId.HasValue)                        
                query = query.Where(c => c.CustomerId == customerId);
            else if(accountId.HasValue)    
                query = query.Where(c => c.Id == accountId);

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

        public async Task<CreditCard> GetCreditCard(int accountId, int? periodId, bool? includeDetail)
        {
            IQueryable<CreditCard> query = _context.CreditCards;

            if(includeDetail.HasValue && includeDetail.Value)
            {
                query = query.Include(e => e.CreditCardHistories)
                             .ThenInclude(x => x.Period);
            }
            
            

            query = query
                        .AsNoTracking()
                        .OrderBy(c => c.Id)
                        .Where(c => c.AccountId == accountId);
            
            if(periodId.HasValue){
                    // query = query.Where(x => x.CreditCardHistories.Any(subitem => subitem.PeriodId == periodId));
            }
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<DebtHistory[]> GetDebtHistory(int accountId, int? periodId)
        {
            IQueryable<DebtHistory> query = _context.DebtHistories;            

            query = query.Include(e => e.Period)
                        .AsNoTracking()
                        .OrderBy(c => c.Id)
                        .Where(c => c.AccountId == accountId);

            if(periodId.HasValue)
            {
                query =query.Where(c => c.PeriodId == periodId );
            }
            return await query.ToArrayAsync();
        }

        public void Update<T>(T entity) where T : class
        {
           _context.Update(entity);
        }
    }
}