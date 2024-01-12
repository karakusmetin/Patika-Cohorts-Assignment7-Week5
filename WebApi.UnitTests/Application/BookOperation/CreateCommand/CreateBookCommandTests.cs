using AutoMapper;
using FluentAssertions;
using System;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DbOperation;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperation.Command
{
	public  class CreateBookCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;
		
		public CreateBookCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Fact]
		public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationExecption_ShouldBeReturn()
		{
			//aarrange - Hazırlık
			var book = new Book()
			{
				Title = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationExecption_ShouldBeReturn",
				PageCount = 100,
				PublisDate = new DateTime(1990, 01, 10),
				GenreId = 1
			};
			_context.Books.Add(book);
			_context.SaveChanges();

			CreateBookCommand command = new CreateBookCommand(_context, _mapper);
			command.Model = new CreateBookModel() { Title = book.Title };

			//act assert - Çalıştırma

			FluentActions
				.Invoking(() => command.Handle())
				.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");
			
			//assert - Doğrulama
		}
		[Fact]
		public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
		{
			//arrange
			CreateBookCommand command = new CreateBookCommand( _context, _mapper);
			CreateBookModel model = new CreateBookModel() { Title = "Hobbit", PageCount = 100, PublisDate = DateTime.Now.Date.AddYears(-10), GenreId = 1 };
			command.Model = model;

			//act
			FluentActions.Invoking(() => command.Handle()).Invoke();

			//assert
			var book = _context.Books.SingleOrDefault(book => book.Title == model.Title);
			book.Should().NotBeNull();
			book.PageCount.Should().Be(model.PageCount);
			book.PublisDate.Should().Be(model.PublisDate);
			book.GenreId.Should().Be(model.GenreId);
		}
	}
}
	