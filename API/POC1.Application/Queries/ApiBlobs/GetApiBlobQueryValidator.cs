using FluentValidation;

namespace POC1.Application.Queries.ApiBlobs
{
    public class GetApiBlobQueryValidator : AbstractValidator<GetApiBlobQuery>
    {
        public GetApiBlobQueryValidator()
        {
            RuleFor(x => x.LogId)
                .NotEmpty().NotNull()
                .WithMessage("Log ID must not be empty");
        }
    }
}
