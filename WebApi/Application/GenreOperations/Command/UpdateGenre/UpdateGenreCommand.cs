using WebApi.DbOperation;

namespace WebApi.Application.GenreOperations.Command.UpdateGenre
{
	public class UpdateGenreCommand
	{
        public int GenreId { get; set; }

		public UpdateGenreModel Model { get; set; }

        private readonly IBookStoreDbContext _dbContext;

		public UpdateGenreCommand(IBookStoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void Handle() 
		{
			var genre = _dbContext.Genres.FirstOrDefault(x=>x.Id == GenreId);
			if (genre == null)
			{
				throw new InvalidOperationException("Güncellenecek Kitap türü bulunamadı.");
			};
			if(_dbContext.Genres.Any(x=>x.Name.ToLower() == genre.Name.ToLower() && x.Id !=GenreId))

			{
				throw new InvalidOperationException("Güncellenecek Kitap türü zaten mevcut.");
			}
			
			genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
			genre.IsActive = Model.IsActive;
			_dbContext.SaveChanges();
			
		}
	}
	public class UpdateGenreModel
	{
		public string Name { get; set; }
		public bool IsActive { get; set; } = true;
	} 
}
