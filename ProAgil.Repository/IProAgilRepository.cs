using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveChangesAsync();
    // ===============================================================================================
                              //EVENTOs
    // ===============================================================================================
         Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes);
         Task<Evento[]>  GetAllEventoAsync(bool includePalestrantes);
         Task<Evento> GetAllEventoAsyncById(int EventoId, bool includePalestrantes);
    
    // ===============================================================================================
                                   //Palestrante
    // ===============================================================================================
    
         Task<Palestrante> GetAllPalestranteAsyncById(int PalestranteId, bool includeEventos = false);
         Task<Palestrante[]> GetAllPalestranteAsyncByName( string name, bool includeEventos= false);

    }
}