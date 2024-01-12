using FluentValidation;

namespace WebApi.Application.BookOperations.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(x => x.BookID).GreaterThan(0);
        }
    }
}
