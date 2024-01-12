

using FluentAssertions;
using WebApi.Application.GenreOperations.Command.UpdateGenre;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.UpdateCommand
{
	public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
	{
		[Theory]
		[InlineData("Gen")]
		[InlineData(" ")]
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
		{
			//arrange
			UpdateGenreCommand command = new UpdateGenreCommand(null);

			command.Model = new UpdateGenreModel() { Name = name, IsActive = true };
			UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
			var errors = validator.Validate(command);

			//act & assert
			errors.Errors.Count.Should().BeGreaterThan(0);
		}

	}
}
