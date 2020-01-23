using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebApi.Dtos
{
    public class RedeSocialDto
    {
        [Required(ErrorMessage =" O campo {0}é Obrigatorio")]
        public string Nome { get; set; }
        [Required(ErrorMessage =" O campo {0}é Obrigatorio")]
        public string URL { get; set; } 
    }
}