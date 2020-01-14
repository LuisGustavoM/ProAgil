using Microsoft.EntityFrameworkCore;
using ProAgil.WebApi.Model;

namespace ProAgil.WebApi.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> Options) : base ( Options ) {}
        
            
        public DbSet<evento> Eventos{ get; set; }
    }
}