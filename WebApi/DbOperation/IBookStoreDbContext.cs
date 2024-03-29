﻿using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperation
{
	public interface IBookStoreDbContext
	{
		DbSet<Book> Books { get; set; }
		DbSet<Genre> Genres { get; set; }

		int SaveChanges();
	}
}
