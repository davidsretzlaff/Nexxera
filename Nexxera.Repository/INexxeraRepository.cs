using System.Threading.Tasks;
using Nexxera.Domain;

namespace Nexxera.Repository
{
    public interface INexxeraRepository
    {
        void Add<T> (T entity) where T: class;
        void Update<T> (T entity) where T: class;
        void Delete<T> (T entity) where T: class;
        Task<bool> SaveChangesAsync();

        // Customer Event
        Task<Customer[]> GetAllCustomer();
        // Account
        Task<Account> GetAccount(int? customerId, int? accountId);
        Task<CreditCard> GetCreditCard(int accountId,int? periodId,bool? includeDetail);

        Task<DebtHistory[]> GetDebtHistory(int accountId,int? periodId);

        Task<Period[]> GetAllPeriod();
    }
}