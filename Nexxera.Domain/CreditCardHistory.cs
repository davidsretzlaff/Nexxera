using System;

namespace Nexxera.Domain
{
    public class CreditCardHistory
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreditCardId { get; set; }
        public decimal Value { get; set; }
        public decimal BalanceCreditCardHistory { get; set; }
    }
}