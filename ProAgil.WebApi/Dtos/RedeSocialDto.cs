using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebApi.Dtos
{
    public class RedeSocialDto
    {
        public int Id { get;  set;}
        [Required(ErrorMessage =" O campo {0}é Obrigatorio")]
        public string Nome { get; set; }
        [Required(ErrorMessage =" O campo {0}é Obrigatorio")]
        public string URL { get; set; } 
    }
}