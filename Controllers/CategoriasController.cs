using ApiPeliculas.Modelos;
using ApiPeliculas.Modelos.Dtos;
using ApiPeliculas.Repositorio;
using ApiPeliculas.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
	[ApiController]
	//[Route("api/[controller]")]
	[Route("api/categorias")]
	
	public class CategoriasController : ControllerBase
	{
		private readonly ICategoriaRepositorio _ctRepo;
		private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepositorio ctRepo, IMapper mapper)
        {
			_ctRepo = ctRepo;
			_mapper = mapper;
        }

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult GetCategorias()
		{
			var listaCategorias = _ctRepo.GetCategorias();

			var listaCategoriasDto = new List<CategoriaDto>();

			foreach(var lista in listaCategorias)
			{
				listaCategoriasDto.Add(_mapper.Map<CategoriaDto>(lista));
			}

			return Ok(listaCategoriasDto);
		}

		[HttpGet("{categoriaId:int}", Name = "GetCategoria")]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetCategoriasId(int categoriaId)
		{
			var itemCategoria = _ctRepo.GetCategoria(categoriaId);

			if(itemCategoria == null)
			{
				return NotFound();
			}

			var itemCategoriaDto = _mapper.Map<CategoriaDto>(itemCategoria);

			return Ok(itemCategoriaDto);
		}
		
		[HttpPost]
		[ProducesResponseType(201, Type = typeof(CategoriaDto))]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult crearCategoria([FromBody]CrearCategoriaDto crearCategoriaDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if(crearCategoriaDto == null)
			{
				return BadRequest(ModelState);
			}

			if (_ctRepo.ExisteCategoria(crearCategoriaDto.Nombre))
			{
				ModelState.AddModelError("","Ya la categoria existe");
				return StatusCode(404, ModelState);
			}

			var categoria = _mapper.Map<Categoria>(crearCategoriaDto);
			if (!_ctRepo.CrearCategoria(categoria))
			{
				ModelState.AddModelError("", $"Algo salio mal guardando el registro{categoria.Nombre}");
				return StatusCode(500, ModelState);	
			}
			return CreatedAtRoute("GetCategoria", new {categoriaId = categoria.Id}, categoria);

		
		}

		[HttpPatch("{categoriaId:int}", Name = "ActualizarPatchCategoria")]
		[ProducesResponseType(201, Type = typeof(CategoriaDto))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult ActualizarPatchCategoria(int categoriaId, [FromBody] CategoriaDto CategoriaDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (CategoriaDto == null || categoriaId != CategoriaDto.Id)
			{
				return BadRequest(ModelState);
			}

			var categoria = _mapper.Map<Categoria>(CategoriaDto);

			if (!_ctRepo.ActualizarCategoria(categoria))
			{
				ModelState.AddModelError("", $"Algo salio mal actualizando el registro{categoria.Nombre}");
				return StatusCode(500, ModelState);
			}
			return NoContent();


		}

		[HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
		[ProducesResponseType(201, Type = typeof(CategoriaDto))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult borrarCategoria(int categoriaId)
		{
			if (!_ctRepo.ExisteCategoria(categoriaId))
			{
				return NotFound();
			}

			var categoria = _ctRepo.GetCategoria(categoriaId);

			if (!_ctRepo.BorrarCategoria(categoria))
			{
				ModelState.AddModelError("", $"Algo salio mal borrando el registro{categoria.Nombre}");
				return StatusCode(500, ModelState);
			}
			return NoContent();


		}

	}
}
