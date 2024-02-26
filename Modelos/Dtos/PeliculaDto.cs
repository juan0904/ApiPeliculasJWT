using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPeliculas.Modelos.Dtos
{
	public class PeliculaDto
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "El nombre es Obligatorio")]
		public string Nombre { get; set; }
		public string RutaImagen { get; set; }
		[Required(ErrorMessage = "La descripcion es Obligatoria")]
		public string Descripcion { get; set; }
		[Required(ErrorMessage = "La duracion es Obligatoria")]
		public int Duracion { get; set; }
		
		public enum TipoClasificacion { Siete, Trece, Dieciseis, Dieciocho }
		public TipoClasificacion Clasificacion { get; set; }
		public DateTime FechaCreacion { get; set; }

		public int categoriaId { get; set; }
	}
}
