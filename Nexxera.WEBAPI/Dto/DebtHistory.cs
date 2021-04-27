using System;
using System.ComponentModel.DataAnnotations;

namespace Nexxera.WEBAPI.Dto
{
    public class DebtHistory
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }

        [Required (ErrorMessage="Descrição é Obrigatório")]
        public string Description { get; set; }
        
        [Required (ErrorMessage="Valor é Obrigatório")]
        public decimal Value { get; set; }
        public decimal BalanceAccountHistory { get; set; }
        [Required (ErrorMessage="Conta é Obrigatório")]
        public int AccountId { get; set; }

        [Required (ErrorMessage="Periodo é Obrigatório")]
        public int PeriodId {get;set;}
        public PeriodDto Period{get;set;}
    }
}