using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperation;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries
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
		public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			//arrange
			var genre = new Genre() { Name = "WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturn", IsActive = true };
			_context.Genres.Add(genre);
			_context.SaveChanges();

			GetGenreDetailQuery command = new GetGenreDetailQuery(_context, _mapper);
			command.GenreId = genre.Id+ 50;
			
			//act & assert
			FluentActions
				.Invoking(() => command.Handle())
				.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");
		}
	}
}
