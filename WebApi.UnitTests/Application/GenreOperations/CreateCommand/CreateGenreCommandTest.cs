using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Command.CreateGenre;
using WebApi.DbOperation;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi.UnitTests.Application.GenreOperations.CreateCommand
{
	public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;
		public CreateGenreCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Fact]
		public void WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			//arrange
			var genre = new Genre() { Name = "WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn",IsActive=true};

			_context.Genres.Add(genre);
			_context.SaveChanges();

			CreateGenreCommand command = new CreateGenreCommand(_context);
			command.Model = new CreateGenreModel() { Name = genre.Name };

			//act & assert
			FluentActions
			.Invoking(() => command.Handle())
				.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü zaten mevcut");
		}

		[Fact]
		public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
		{
			//arrenge
			CreateGenreCommand command = new CreateGenreCommand(_context);
			CreateGenreModel model = new CreateGenreModel() { Name = "WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn" };
			command.Model = model;

			//act
			FluentActions
				.Invoking(() => command.Handle()).Invoke();

			var genre = _context.Genres.SingleOrDefault(genre => genre.Name == model.Name);

			//assert
			genre.Should().NotBeNull();
			genre.Name.Should().Be(model.Name);
			genre.IsActive.Should();
			genre.Id.Should().BeGreaterThan(0);
		}
	}
}
