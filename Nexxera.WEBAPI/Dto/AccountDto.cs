using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nexxera.WEBAPI.Dto
{
    public class AccountDto
    {
        public int Id { get; set; }

         [Required (ErrorMessage="Saldo é Obrigatório")]
        public decimal Balance{get;set;}

        [Required (ErrorMessage="Cliente é Obrigatório")]
        public int CustomerId { get; set; }

        public CustomerDto Customer { get; set; }

         public List<CreditCardDto> CreditCards { get; set; }
         public List<DebtHistoryDto> DebtHistories { get; set; }
    }
}