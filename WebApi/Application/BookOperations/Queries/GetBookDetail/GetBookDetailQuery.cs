
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperation;

namespace WebApi.Application.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper mapper;

        public int BookID { get; set; }
        public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }
        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Include(x=>x.Genre).Where(book => book.Id == BookID).SingleOrDefault();
            if (book == null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }
            BookDetailViewModel vm = mapper.Map<BookDetailViewModel>(book);
            return vm;
        }

    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
