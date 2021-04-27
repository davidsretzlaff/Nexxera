using System;
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
    }
}