using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.GetBookDetail;
using WebApi.DbOperation;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperation.Queries
{
	
	public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;
		public GetGenreDetailQueryTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Fact]
		public void WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			//arrange
			var book = new Book() { Title = "WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublisDate = new DateTime(2000, 01, 20), GenreId = 1 };
			_context.Books.Add(book);
			_context.SaveChanges();

			GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
			command.BookID = book.Id + 5;

			//act & assert
			FluentActions
				.Invoking(() => command.Handle())
				.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı.");
		}
	}
}
