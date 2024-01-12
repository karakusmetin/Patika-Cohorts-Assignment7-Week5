

using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Command.UpdateGenre;
using WebApi.DbOperation;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.UpdateCommand
{
	public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;
		public UpdateGenreCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Fact]
		public void WhenAlreadyExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			//arrange
			var genre = new Genre() { Name = "WhenAlreadyExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn", IsActive = true };
			_context.Genres.Add(genre);
			_context.SaveChanges();

			UpdateGenreCommand command = new UpdateGenreCommand(_context);
			command.GenreId = genre.Id + 10;

			command.Model = new UpdateGenreModel() { Name = genre.Name + "5", IsActive = genre.IsActive };

			//act & assert
			FluentActions
				.Invoking(() => command.Handle())
				.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek Kitap türü bulunamadı.");
		}

		[Fact]
		public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			//arrange
			var genre = new Genre() { Name = "WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn", IsActive = true };
			_context.Genres.Add(genre);
			_context.SaveChanges();

			UpdateGenreCommand command = new UpdateGenreCommand(_context);
			command.GenreId = genre.Id;
			command.Model = new UpdateGenreModel() { Name = genre.Name, IsActive = genre.IsActive };

			//act & assert
			FluentActions
				.Invoking(() => command.Handle())
				.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek Kitap türü zaten mevcut.");
		}
	}
}
