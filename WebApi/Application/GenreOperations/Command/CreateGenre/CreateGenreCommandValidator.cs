using FluentValidation;

namespace WebApi.Application.GenreOperations.Command.CreateGenre
{
	public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
	{
		public CreateGenreCommandValidator()
		{
			RuleFor(x=>x.Model.Name).NotEmpty().MinimumLength(4) ;
		}
	}
}
