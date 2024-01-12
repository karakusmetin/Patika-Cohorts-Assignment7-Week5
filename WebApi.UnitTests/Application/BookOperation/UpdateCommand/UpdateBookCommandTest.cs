using AutoMapper;
using FluentAssertions;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperation;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperation.UpdateCommand
{
	public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;
		public UpdateBookCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Fact]
		public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			//arrange
			var book = new Book() { Title = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 1000, PublisDate = new DateTime(2000, 01, 20), GenreId = 1 };
			_context.Books.Add(book);
			_context.SaveChanges();

			UpdateBookCommand command = new UpdateBookCommand(_context);
			command.BookId = book.Id+5;
			command.Model = new UpdateBookModel() { Title = book.Title };

			//act & assert
			FluentActions
				.Invoking(() => command.Handle())
				.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");
		}
	}
}
