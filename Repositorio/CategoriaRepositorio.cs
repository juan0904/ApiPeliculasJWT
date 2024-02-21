using ApiPeliculas.Data;
using ApiPeliculas.Modelos;
using ApiPeliculas.Repositorio.IRepositorio;

namespace ApiPeliculas.Repositorio
{
	public class CategoriaRepositorio : ICategoriaRepositorio
	{
		private readonly ApplicationDbContext _bd;

        public CategoriaRepositorio(ApplicationDbContext bd)
        {
			_bd = bd;
        }
        public bool ActualizarCategoria(Categoria categoria)
		{
			categoria.FechaCreacion  = DateTime.Now;
			_bd.Categorias.Update(categoria);
			return Guardar();
		}

		public bool BorrarCategoria(Categoria categoria)
		{
			_bd.Categorias.Remove(categoria);
			return Guardar();
		}

		public bool CrearCategoria(Categoria categoria)
		{
			categoria.FechaCreacion = DateTime.Now;
			_bd.Categorias.Add(categoria);
			return Guardar();
		}

		public bool ExisteCategoria(string categoria)
		{
			bool valor = _bd.Categorias.Any(c => c.Nombre.ToLower().Trim() == categoria.ToLower().Trim());
			return valor;
		}

		public bool ExisteCategoria(int id)
		{
			return _bd.Categorias.Any(c => c.Id == id);
		}

			public Categoria GetCategoria(int categorId)
			{
				return _bd.Categorias.FirstOrDefault(c => c.Id == categorId);
			}

		public ICollection<Categoria> GetCategorias()
		{	
			return _bd.Categorias.OrderBy(c => c.Nombre).ToList();
		}

		public bool Guardar()
		{
			return _bd.SaveChanges() >= 0 ? true : false;

		}
	}
}
