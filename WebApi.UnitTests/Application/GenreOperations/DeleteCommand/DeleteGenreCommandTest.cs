using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Command.DeleteGenre;
using WebApi.DbOperation;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.DeleteCommand
{
	public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public DeleteGenreCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Theory]
		[InlineData(50)]
		[InlineData(150)]
		public void WhenThereIsNoGenre_InvalidOperationException_ShouldBeReturn(int genreId)
		{
			//arrange
			DeleteCommandGenre deleteCommand = new DeleteCommandGenre(_context);
			deleteCommand.Id = genreId;

			//act & assert
			FluentActions.Invoking(() => deleteCommand.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı"); ;
		}
	}
}
