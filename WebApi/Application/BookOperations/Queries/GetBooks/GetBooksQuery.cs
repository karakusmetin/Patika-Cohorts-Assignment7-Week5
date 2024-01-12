﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperation;

namespace WebApi.Application.BookOperations.Queries.GetBooks
{
	public class GetBooksQuery
    {
        private readonly IBookStoreDbContext dbContext;
        private readonly IMapper mapper;
        public GetBooksQuery(IBookStoreDbContext _dbContext, IMapper mapper)
        {
            dbContext = _dbContext;
            this.mapper = mapper;
        }
        public List<BooksViewModel> Handle()
        {
            var books = dbContext.Books.Include(x => x.Genre).OrderBy(x => x.Id).ToList();
            List<BooksViewModel> vm = mapper.Map<List<BooksViewModel>>(books);
            //foreach (var book in books) 
            //{
            //	vm.Add(new BooksViewModel()
            //	{
            //		Title = book.Title,
            //		Genre = ((GenreEnum)book.GenreId).ToString(),
            //		PublisDate = book.PublisDate.Date.ToString("dd/MM/yyyy"),
            //		PageCount = book.PageCount,
            //	});
            //}
            return vm;
        }
    }

    public class BooksViewModel
    {
        public string Genre { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublisDate { get; set; }

    }


}
