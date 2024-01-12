using AutoMapper;
using WebApi.DbOperation;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommand
    {
        private readonly IBookStoreDbContext _dbContext;
		public CreateGenreModel Model { get; set; }

		public CreateGenreCommand(IBookStoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x=>x.Name ==Model.Name);
            if (genre != null) 
            {
                throw new InvalidOperationException("Kitap türü zaten emvcut");
            }
            genre = new Genre();
            genre.Name = Model.Name;
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
        }
        
    }
    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}
