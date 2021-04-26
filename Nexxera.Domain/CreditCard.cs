namespace Nexxera.Domain
{
    public class CreditCard
    {
        public int Id { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal  Balance { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int PeriodId { get; set; }
        public Period Period { get; set; }
    }
}