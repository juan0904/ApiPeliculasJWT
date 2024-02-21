using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Modelos.Dtos
{
	public class CategoriaDto
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "El nombre es Obligatorio")]
		[MaxLength(60, ErrorMessage = "El numero maximo de caracteres es de 60!")]
		public string Nombre { get; set; }
	}
}
