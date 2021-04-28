using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nexxera.WEBAPI.Dto
{
    public class CreditCardDto
    {
        public int Id { get; set; }
        
        [Required (ErrorMessage="O Limite é Obrigatório")]
        public decimal CreditLimit { get; set; }        

        [Required (ErrorMessage="Saldo é Obrigatório")]
        public decimal  Balance { get; set; }
        
        [Required (ErrorMessage="Conta é Obrigatório")]
        
        public List<CreditCardHistoryDto> CreditCardHistories { get; set; }
 
    }
}