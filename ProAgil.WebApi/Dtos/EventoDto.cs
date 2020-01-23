using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProAgil.Domain;

namespace ProAgil.WebApi.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Campo Obrigatorio")]
        [StringLength (100, MinimumLength =3, ErrorMessage ="Local é de 3 a 100 characters")]
        public string Local { get; set; }  
        public string DataEvento { get; set; }
        [Required (ErrorMessage ="O Tema deve ser preenchido")]
        public string Tema { get; set; }
        
        [Range(1,1500, ErrorMessage = "Quantidade de Pessaos deve ser entre 1 e 1500")]
        public int QtdPessoas { get; set; }
        public string ImagemURL { get; set; }  
        [Phone]
        public string Telefone { get; set; }
        [EmailAddress]
        public string email { get; set; }
        public List<LoteDto> Lotes {get; set;}
        public List<RedeSocialDto> RedesSociais { get; set; }
        public List<PalestranteDto> Palestrantes { get; set; }
    }
}