using System.Collections.Generic;

namespace Nexxera.Domain
{
    public class CreditCard
    {
        public int Id { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal  Balance { get; set; }
        public int AccountId { get; set; }
        
        public List<CreditCardHistory> CreditCardHistories { get; set; }
        
    }
}