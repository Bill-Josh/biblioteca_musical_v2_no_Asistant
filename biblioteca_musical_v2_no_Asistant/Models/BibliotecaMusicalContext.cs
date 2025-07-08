using biblioteca_musical_v2_no_Asistant.Models;
using Microsoft.EntityFrameworkCore;

namespace biblioteca_musical_v2_no_Asistant.Models
{
    public class BibliotecaMusicalContext : DbContext
    {
        public BibliotecaMusicalContext(DbContextOptions<BibliotecaMusicalContext> opts)
            : base(opts) { }

        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Cancion> Canciones { get; set; }
        public DbSet<ListaReproduccion> Listas { get; set; }
        public DbSet<ListaReproduccionCancion> ListaCanciones { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Cancion ↔ Genero
            mb.Entity<Cancion>()
              .HasOne(c => c.IdGeneroNavigation)
              .WithMany()
              .HasForeignKey(c => c.IdGenero);

            // Cancion ↔ Artista
            mb.Entity<Cancion>()
              .HasOne(c => c.IdArtistaNavigation)
              .WithMany()
              .HasForeignKey(c => c.IdArtista);

            // ListaReproduccionCancion ↔ Cancion & Lista
            mb.Entity<ListaReproduccionCancion>()
              .HasOne(lrc => lrc.Lista)
              .WithMany()
              .HasForeignKey(lrc => lrc.IdLista);

            mb.Entity<ListaReproduccionCancion>()
              .HasOne(lrc => lrc.Cancion)
              .WithMany()
              .HasForeignKey(lrc => lrc.IdCancion);
        }
    }
}
