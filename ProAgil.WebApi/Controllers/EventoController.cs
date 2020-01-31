using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.WebApi.Dtos;

namespace ProAgil.WebApi.Controllers {

    [Route ("api/[controller]")]
    
    [ApiController] // API CONTROLLER VALIDA OS DATA ANNOTATIONS
    
    public class EventoController : ControllerBase 
    {
        private readonly IProAgilRepository _repo;
        private readonly IMapper _mapper;
        public EventoController (IProAgilRepository repo, IMapper mapper) {
            
            _mapper = mapper;
            _repo = repo;
        }

        //Retorna Todos os eventos ( AUTO MAPPER FILTRANDO OS EVENTOS)
        [HttpGet]
        
        public async Task<IActionResult> Get () {
            try {
                var eventos = await _repo.GetAllEventoAsync (true);
                var results = _mapper.Map<EventoDto[]>(eventos);
                return Ok (results);

            } catch (System.Exception) {

                return this.StatusCode (StatusCodes.Status500InternalServerError, " Erro ao receber a requisição");
            }
        }

        //Retorna Todos os eventos filtrados pelo ID (AUTO MAPPER FILTRANDO OS EVENTOS RETORNADOS PELO ID)

        [HttpGet ("{EventoId}")]
        public async Task<IActionResult> Get (int EventoId) {
            try {
                var evento = await _repo.GetAllEventoAsyncById (EventoId, true);

                var results = _mapper.Map<EventoDto>(evento);

                return Ok (results);

            } catch (System.Exception) {

                return this.StatusCode (StatusCodes.Status500InternalServerError, "Erro ao receber a requisição");
            }
        }

        //Retorna Todos os eventos filtrados pelo TEMA

        [HttpGet ("getByTema/{tema}")]
        public async Task<IActionResult> Get (string tema) {
            try {
                
                var eventos = await _repo.GetAllEventoAsyncByTema (tema, true);
                var results = _mapper.Map<EventoDto[]>(eventos);

                return Ok (results);

            } catch (System.Exception) {

                return this.StatusCode (StatusCodes.Status500InternalServerError, " Erro ao receber a requisição");
            }  
        }

        // UPLOAD DA IMAGEM NO CAMPO EVENTO
        [HttpPost("upload")]
        public IActionResult upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Imagens");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, filename.Replace("\"", " ").Trim());
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Ok();
            }
            catch (System.Exception)
            {

                return BadRequest("Erro ao tentar fazer o upload");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post (EventoDto model) {
            try {
                var evento = _mapper.Map<Evento>(model);
                _repo.Add(evento);
                     if (await _repo.SaveChangesAsync ()) {

                    return Created ($"/api/evento/{model.Id}", model);
                }

            } catch (System.Exception) {

                return this.StatusCode (StatusCodes.Status500InternalServerError, "Erro ao receber a requisição");
            }

            return BadRequest ();
        }

        [HttpPut ("{EventoId}")]
        public async Task<IActionResult> Put (int EventoId, EventoDto model) {
            try {
                var evento = await _repo.GetAllEventoAsyncById (EventoId, false);
                if(evento ==null) return NotFound();
                _mapper.Map(model, evento);
                _repo.Update (evento);

                if (await _repo.SaveChangesAsync ()) {

                    return Created ($"/api/evento/{model.Id}",_mapper.Map<EventoDto> (evento));
                }

            } catch (System.Exception Erro) {

                return this.StatusCode (StatusCodes.Status500InternalServerError, " Erro ao receber a requisição" + Erro.ToString ());
            }

            return BadRequest ();
        }

        [HttpDelete ("{EventoId}")]
        public async Task<IActionResult> Delete (int EventoId, EventoDto model) {
            try {

                var evento = await _repo.GetAllEventoAsyncById (EventoId, false);
                if(evento == null) return NotFound();

                var idLotes = new List<int> ();
                var idRedesSociais = new List<int>();
                
                model.Lotes.ForEach(item => idLotes.Add(item.Id));
                model.RedesSociais.ForEach(item => idRedesSociais.Add(item.Id));
  
                var lotes = evento.Lotes.Where(lote => !idLotes.Contains(lote.Id)).ToArray();
                var RedesSociais = evento.RedesSociais.Where(rede => !idRedesSociais.Contains(rede.Id)).ToArray();

                if(lotes.Length > 0)  _repo.DeleteRange(lotes);
                   
                if(RedesSociais.Length > 0) _repo.DeleteRange(RedesSociais);

                _mapper.Map(model,evento);

                _repo.Update (evento);

                if (await _repo.SaveChangesAsync ()) {

                    return Ok ();
                }

            } catch (System.Exception) {

                return this.StatusCode (StatusCodes.Status500InternalServerError, "Erro ao receber a requisição");
            }

            return BadRequest ();
        }

    }
}