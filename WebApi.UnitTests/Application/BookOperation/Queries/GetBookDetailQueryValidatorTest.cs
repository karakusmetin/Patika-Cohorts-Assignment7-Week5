using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.GetBookDetail;
using WebApi.DbOperation;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperation.Queries
{
	public class GetBookrDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;
		public GetBookrDetailQueryValidatorTest(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;

		}
		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void WhenBookIdIsGivenLowerThanZero_Validator_ShouldBeReturnError(int bookId)
		{
			//arrange
			GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);

			query.BookID = bookId;
			GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
			var errors = validator.Validate(query);

			//act & assert
			errors.Errors.Count.Should().BeGreaterThan(0);
		}
	}
}
