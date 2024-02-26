using ApiPeliculas.Data;
using ApiPeliculas.Modelos;
using ApiPeliculas.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Repositorio
{
	public class PeliculaRepositorio : IPeliculaRepositorio
	{
		private readonly ApplicationDbContext _bd;

        public PeliculaRepositorio(ApplicationDbContext bd)
        {
			_bd = bd;
        }
        public bool ActualizarPelicula(Pelicula pelicula)
		{
			pelicula.FechaCreacion  = DateTime.Now;
			_bd.Pelicula.Update(pelicula);
			return Guardar();
		}

		public bool BorrarPelicula(Pelicula pelicula)
		{
			_bd.Pelicula.Remove(pelicula);
			return Guardar();
		}


		public bool CrearPelicula(Pelicula pelicula)
		{
			pelicula.FechaCreacion = DateTime.Now;
			_bd.Pelicula.Add(pelicula);
			return Guardar();
		}

		public bool ExistePelicula(string pelicula)
		{
			bool valor = _bd.Pelicula.Any(c => c.Nombre.ToLower().Trim() == pelicula.ToLower().Trim());
			return valor;
		}

		public bool ExistePelicula(int id)
		{
			return _bd.Pelicula.Any(c => c.Id == id);
		}

		public Pelicula GetPelicula(int peliculaId)
		{
				return _bd.Pelicula.FirstOrDefault(c => c.Id == peliculaId);
		}

		public ICollection<Pelicula> GetPeliculas()
		{	
			return _bd.Pelicula.OrderBy(c => c.Nombre).ToList();
		}

		public ICollection<Pelicula> GetPeliculasEnCategoria(int catId)
		{
			return _bd.Pelicula.Include(ca => ca.Categoria.Id).Where(c => c.categoriaId == catId).ToList();
		}

		public ICollection<Pelicula> BuscarPelicula(string nombre)
		{
			IQueryable<Pelicula> query = _bd.Pelicula;

			if (!string.IsNullOrEmpty(nombre))
			{
				query = query.Where(c => c.Nombre.Contains(nombre) || c.Descripcion.Contains(nombre));
			}

			return query.ToList();
		}

		public bool Guardar()
		{
			return _bd.SaveChanges() >= 0 ? true : false;

		}
	}
}
