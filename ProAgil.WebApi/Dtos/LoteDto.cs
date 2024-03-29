using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebApi.Dtos
{
    public class LoteDto
    {
        public int Id { get; set; }   
        [Required]
        public string Nome { get; set; }    

        [Required]
        public decimal Preco { get; set; } 
        [Required]

        public string DataInicio { get; set; }   

        public string DataFim { get; set; }  
       
        [Range(10,1500)]
        public int quantidade { get; set; }
        
    }
}