using System.ComponentModel.DataAnnotations;

namespace Nexxera.WEBAPI.Dto
{
    public class PeriodDto
    {
        public int Id { get; set; }
        [Required (ErrorMessage="Descrição é Obrigatório")]
        public string Description { get; set; }
    }
}