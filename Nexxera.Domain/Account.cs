using System.Collections.Generic;

namespace Nexxera.Domain
{
    public class Account 
    {
        public int Id { get; set; }
        public decimal Balance{get;set;}

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<CreditCard> CreditCards { get; set; }
        public List<DebtHistory> DebtHistories { get; set; }
    }
}