using ApiPeliculas.Modelos;
using ApiPeliculas.Modelos.Dtos;
using AutoMapper;

namespace ApiPeliculas.PeliculasMapper
{
	public class PeliculasMapper : Profile
	{
        public PeliculasMapper()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
			CreateMap<Categoria, CrearCategoriaDto>().ReverseMap();
			CreateMap<Pelicula, PeliculaDto>().ReverseMap();
		}
    }
}
