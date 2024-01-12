using AutoMapper;
using System.Linq;
using WebApi.DbOperation;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
	public class GetGenreDetailQuery
	{
		public int GenreId { get; set; }

        private readonly IBookStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
		{
			_mapper = mapper;
			_context = context;
		}

		public GenresDetailViewModel Handle()
		{
			var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
			if (genre == null)
			{
				throw new InvalidCastException("Kitap türü bulunamadı");
			}
			GenresDetailViewModel returnObj = _mapper.Map<GenresDetailViewModel>(genre);
			return returnObj;
		}
	}

	public class GenresDetailViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
