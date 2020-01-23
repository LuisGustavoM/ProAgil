using System.Collections.Generic;
using ProAgil.Domain;

namespace ProAgil.WebApi.Dtos
{
    public class PalestranteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }    

        public string MiniCurriculo { get; set; }
        public string ImagemURL { get; set; }   

        public string Telefone { get; set; }

        public string email { get; set; }   
        public List<RedeSocialDto> RedesSociais { get; set; }
        
        public List<PalestranteDto> Eventos{ get; set; }
    }
}