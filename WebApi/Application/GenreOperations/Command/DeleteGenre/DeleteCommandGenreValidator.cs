using FluentValidation;

namespace WebApi.Application.GenreOperations.Command.DeleteGenre
{
	public class DeleteCommandGenreValidator : AbstractValidator<DeleteCommandGenre>
	{
		public DeleteCommandGenreValidator()
		{
			RuleFor(x=>x.Id).NotEmpty().GreaterThan(0);
		}
	}
}
