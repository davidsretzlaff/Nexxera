using System.ComponentModel.DataAnnotations;

namespace Nexxera.WEBAPI.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        
        [Required (ErrorMessage="O Campo {0} é Obrigatório")]
        public string Name { get; set; }

        [Required (ErrorMessage="O Campo {0} é Obrigatório")]
        [StringLength (11, MinimumLength=11, ErrorMessage="Cpf precisa ter 11 caracteres")]
        public string Cpf { get; set; }
    }
}