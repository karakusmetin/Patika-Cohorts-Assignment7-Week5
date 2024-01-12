using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperation.DeleteCommand
{
	public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
	{

		[Theory]
		[InlineData(-10)]
		[InlineData(-5)]
		[InlineData(0)]
		public void WhenInvalidBookIdInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId)
		{
			//arrange
			DeleteBookCommand command = new DeleteBookCommand(null);

			command.BookId = bookId;
			DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
			var errors = validator.Validate(command);

			//act & assert
			errors.Errors.Count.Should().BeGreaterThan(0);
		}
	}
}
