using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nexxera.WEBAPI.Dto
{
    public class CreditCardHistoryDto
    {
        public int Id { get; set; }        
        public DateTime CreateDate { get; set; }        
        [Required (ErrorMessage="Valor é Obrigatório")]
        public decimal Value { get; set; }        
        public decimal BalanceCreditCardHistory { get; set; }
        public int CreditCardId { get; set; }

        [Required (ErrorMessage="Periodo é Obrigatório")]
        public int PeriodId { get; set; }
        
         public PeriodDto Period { get; set; }
         public List<CreditCardHistoryDto> CreditCardHistories { get; set; }

    }
}