using FluentAssertions;
using WebApi.Application.GenreOperations.Command.DeleteGenre;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.DeleteCommand
{
	public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
	{

		[Theory]
		[InlineData(-5)]
		public void WhenInvalidGenreIdInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId)
		{
			//arrange
			DeleteCommandGenre command = new DeleteCommandGenre(null);

			command.Id = genreId;
			DeleteCommandGenreValidator validator = new DeleteCommandGenreValidator();
			var errors = validator.Validate(command);

			//act & assert
			errors.Errors.Count.Should().BeGreaterThan(0);
		}
	}
}
