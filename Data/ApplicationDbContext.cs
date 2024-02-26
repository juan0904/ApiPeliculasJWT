using ApiPeliculas.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Data
{
    public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		
		}

		public DbSet<Categoria> Categorias { get; set; }
		public DbSet<Pelicula> Pelicula { get; set; }

	}
}
