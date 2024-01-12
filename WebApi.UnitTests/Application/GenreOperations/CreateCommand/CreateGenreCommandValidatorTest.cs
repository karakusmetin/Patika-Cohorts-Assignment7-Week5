using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.GetBookDetail;
using WebApi.Application.GenreOperations.Command.CreateGenre;
using WebApi.DbOperation;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.CreateCommand
{
	public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
	{

		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;
		public CreateGenreCommandValidatorTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Theory]
		[InlineData("Bir")]
		[InlineData(" ")]
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
		{
			//arrange
			CreateGenreCommand command = new CreateGenreCommand(_context);

			command.Model = new CreateGenreModel() { Name = name };
			CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
			var errors = validator.Validate(command);

			//act & assert
			errors.Errors.Count.Should().BeGreaterThan(0);
		}
	}
}
