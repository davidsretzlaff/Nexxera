using System;

namespace Nexxera.Domain
{
    public class DebtHistory
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public decimal BalanceAccountHistory { get; set; }
        public int AccountId { get; set; }           
        public int PeriodId {get;set;}
        public Period Period{get;set;}
    }
}