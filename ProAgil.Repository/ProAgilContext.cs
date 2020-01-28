using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProAgil.Domain.Identity;

namespace ProAgil.Repository
{
    public class ProAgilContext : IdentityDbContext<User, Role, int,
                                    IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                    IdentityRoleClaim<int>, IdentityUserToken<int>>
    
    {
        public ProAgilContext(DbContextOptions<ProAgilContext> Options) : base ( Options ) {}
        
        public DbSet<Evento> Eventos{ get; set; }
        
        public DbSet<Palestrante> Palestrantes{ get; set; }

        public DbSet<PalestranteEvento> PalestrantesEventos{ get; set; }

        public DbSet<Lote> Lotes{ get; set; }
    
        public DbSet<RedeSocial> RedeSociais{ get; set; }

        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            base.OnModelCreating(ModelBuilder);
            ModelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                userRole.HasOne(ur => ur.Role)
                .WithMany(r =>r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                userRole.HasOne(ur => ur.User)
                .WithMany(r =>r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });
            ModelBuilder.Entity<PalestranteEvento>().HasKey
            (PE => new{ PE.EventoId, PE.PalestranteId});
        }

    }
}