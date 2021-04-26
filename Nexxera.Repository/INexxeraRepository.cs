using System.Threading.Tasks;
using Nexxera.Domain;

namespace Nexxera.Repository
{
    public interface INexxeraRepository
    {
        void Add<T> (T entity) where T: class;
        void Update<T> (T entity) where T: class;
        void Delete<T> (T entity) where T: class;

        void DeleteRange<T> (T[] entity) where T : class;
        Task<bool> SaveChangesAsync();

        // Customer Event
        Task<Customer[]> GetAllCustomer();
        Task<Customer> GetCustomerById(int id);
        
        // Account
        Task<Account> GetAccount(int customerId);

        Task<CreditCard> GetCreditCard(int accountId,int periodId);

        Task<DebtHistory> GetDebtHistory(int accountId,int periodId);

        Task<Period[]> GetAllPeriod();
    }
}