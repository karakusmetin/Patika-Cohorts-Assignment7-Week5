﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.BookOperations.Common;
using WebApi.DbOperation;

namespace WebApi.UnitTests.TestSetup
{
	public class CommonTestFixture
	{
		public BookStoreDbContext Context { get; set; }
		public IMapper Mapper { get; set; }
		public CommonTestFixture() 
		{
			var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDb").Options;
			
			Context = new BookStoreDbContext(options);
			Context.Database.EnsureCreated();
			Context.AddBooks();
			Context.AddGenres();
			Context.SaveChanges();

			Mapper = new MapperConfiguration(config => { config.AddProfile<MappingProfile>(); }).CreateMapper();
		}
	}
}
