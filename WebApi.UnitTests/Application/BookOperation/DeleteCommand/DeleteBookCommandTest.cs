using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbOperation;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperation.DeleteCommand
{
	public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public DeleteBookCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Theory]
		[InlineData(100)]
		[InlineData(150)]
		public void WhenThereIsNoBook_InvalidOperationException_ShouldBeReturn(int bookId)
		{
			//arrange
			DeleteBookCommand deleteCommand = new DeleteBookCommand(_context);
			deleteCommand.BookId = bookId;

			//act & assert
			FluentActions.Invoking(() => deleteCommand.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinicek bulunamadı"); ;
		}
	}
}
