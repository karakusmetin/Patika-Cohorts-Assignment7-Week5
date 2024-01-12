using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DbOperation;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperation.CreateCommand
{
	public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
	{
		
		[Theory]
		[InlineData("Lord of The Rings",0,0)]
		[InlineData("Lord of The Rings",0,1)]
		[InlineData("Lord of The Rings",100,0)]
		[InlineData("",0,0)]
		[InlineData("",100,1)]
		[InlineData("",0,1)]
		[InlineData("lor",100,1)]
		[InlineData("lord",100,0)]
		[InlineData("lord",0,1)]
		[InlineData(" ",100,1)]
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount,int genreId)
		{
			//aarrange - Hazırlık
			CreateBookCommand command = new CreateBookCommand(null, null);
			command.Model = new CreateBookModel()
			{
				Title = title,
				PageCount = pageCount,
				PublisDate = DateTime.Now.AddYears(-1),
				GenreId = genreId
			};

			//act assert - Çalıştırma
			CreateBookCommandValidator validator = new CreateBookCommandValidator();
			var result = validator.Validate(command);

			//assert - Doğrulama
			result.Errors.Count().Should().BeGreaterThan(0);
		}

		[Fact]

		public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
		{
			CreateBookCommand command = new CreateBookCommand(null, null);
			command.Model = new CreateBookModel()
			{
				Title = "Lord Of The Rings",
				PageCount = 100,
				PublisDate = DateTime.Now.Date,
				GenreId = 1
			};

			CreateBookCommandValidator validator = new CreateBookCommandValidator();
			var result = validator.Validate(command);

			result.Errors.Count().Should().BeGreaterThan(0);
		}
		[Fact]

		public void WhenValidInput_Validator_ShouldNotBeReturnError()
		{
			CreateBookCommand command = new CreateBookCommand(null, null);
			command.Model = new CreateBookModel()
			{
				Title = "Lord Of The Ringss",
				PageCount = 100,
				PublisDate = DateTime.Now.AddYears(-2),
				GenreId = 1
			};

			CreateBookCommandValidator validator = new CreateBookCommandValidator();
			var result = validator.Validate(command);

			result.Errors.Count().Should().Equals(0);
		}
	}
}
