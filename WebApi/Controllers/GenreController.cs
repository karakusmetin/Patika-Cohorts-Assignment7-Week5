using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Command.CreateGenre;
using WebApi.Application.GenreOperations.Command.DeleteGenre;
using WebApi.Application.GenreOperations.Command.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DbOperation;

namespace WebApi.Controllers
{
	[Route("api/[controller]s")]
	[ApiController]
	public class GenreController : ControllerBase
	{
		private readonly IBookStoreDbContext _dbContext;
		private readonly IMapper _mapper;

		public GenreController(IBookStoreDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetGenres()
		{
			GetGenresQuery query = new GetGenresQuery(_dbContext, _mapper);
			var obj = query.Handle();
			return Ok(obj);
		}

		[HttpGet("{id}")]
		public IActionResult GetGenreDetail(int id)
		{
			GetGenreDetailQuery query = new GetGenreDetailQuery(_dbContext, _mapper);
			query.GenreId = id;

			GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
			validator.ValidateAndThrow(query);
			var obj = query.Handle();
			return Ok(obj);
		}
		[HttpPost]
		public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
		{
			CreateGenreCommand command = new CreateGenreCommand(_dbContext);
			command.Model = newGenre;
			CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
			validator.ValidateAndThrow(command);

			command.Handle();
			return Ok();
		}
		[HttpPut("{}")]
		public IActionResult UpdateGenre(int id,[FromBody] UpdateGenreModel updateGenre)
		{
			UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
			command.GenreId=id;
			command.Model = updateGenre;

			UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
			validator.ValidateAndThrow(command);

			command.Handle();
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteGenre(int id)
		{
			DeleteCommandGenre command = new DeleteCommandGenre(_dbContext);
			command.Id=id;

			DeleteCommandGenreValidator validator = new DeleteCommandGenreValidator();
			validator.ValidateAndThrow(command);

			command.Handle();
			return Ok();
		}

	}
}
