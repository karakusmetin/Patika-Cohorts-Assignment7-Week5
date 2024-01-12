using AutoMapper;
using FluentAssertions;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperation;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperation.UpdateCommand
{
	public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
	{
		[Theory]
		[InlineData("Lord Of The", 0)]
		[InlineData("", 0)]
		[InlineData("", 1)]
		
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int genreId)
		{
			//arrange
			UpdateBookCommand command = new UpdateBookCommand(null);

			command.Model = new UpdateBookModel() { Title = title, GenreId = genreId };
			UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
			var errors = validator.Validate(command);

			//act & assert
			errors.Errors.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
		{
			//arrange
			UpdateBookCommand command = new UpdateBookCommand(null);

			command.Model = new UpdateBookModel() { Title = "Lord Of The Rings", GenreId = 1 };
			UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
			var errors = validator.Validate(command);

			//act & assert
			errors.Errors.Count.Should().BeGreaterThan(0);
		}
	}
}
