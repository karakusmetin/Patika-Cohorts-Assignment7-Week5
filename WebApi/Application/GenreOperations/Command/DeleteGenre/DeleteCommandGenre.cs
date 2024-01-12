using WebApi.DbOperation;

namespace WebApi.Application.GenreOperations.Command.DeleteGenre
{
	public class DeleteCommandGenre
	{
        public int Id { get; set; }
        private readonly IBookStoreDbContext _dbContext;

		public DeleteCommandGenre(IBookStoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void Handle() 
		{
			var genre = _dbContext.Genres.FirstOrDefault(x=>x.Id == Id);
			if (genre == null)
			{
				throw new InvalidOperationException("Kitap türü bulunamadı");
			}
			_dbContext.Genres.Remove(genre);
			_dbContext.SaveChanges();
		}
	}
}
