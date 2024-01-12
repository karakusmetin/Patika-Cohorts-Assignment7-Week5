using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DbOperation;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace WebApi.Controllers
{
    [Route("[controller]s")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly IBookStoreDbContext context;

		private readonly IMapper mapper;
		
		public BookController(IBookStoreDbContext dbContext, IMapper _mapper)
		{
			context = dbContext;
			mapper = _mapper;
		}

		[HttpGet]
		public IActionResult GetBooks()
		{
			GetBooksQuery query = new GetBooksQuery(context, mapper);
			var result = query.Handle();
			return Ok(result);

		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			BookDetailViewModel result;

			GetBookDetailQuery query = new GetBookDetailQuery(context, mapper);
			query.BookID = id;
			GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
			validator.ValidateAndThrow(query);
			result = query.Handle();

			return Ok(result);
		}

		[HttpPost]
		public IActionResult AddBook([FromBody] CreateBookModel newBook)
		{
			CreateBookCommand command = new CreateBookCommand(context, mapper);

			command.Model = newBook;
			CreateBookCommandValidator validator = new CreateBookCommandValidator();
			ValidationResult result = validator.Validate(command);
			validator.ValidateAndThrow(command);
				
			return Ok();
		}

		[HttpPut("{id}")]
		public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
		{
			
				UpdateBookCommand command = new UpdateBookCommand(context);
				command.BookId = id;
				command.Model = updatedBook;
				UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
				ValidationResult result = validator.Validate(command);
				command.Handle();
			
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteBook(int id)
		{
			DeleteBookCommand command = new DeleteBookCommand(context);
			command.BookId = id;
			DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
			validator.ValidateAndThrow(command);
			command.Handle();
		
			return Ok();
		}

	}
}


