using FluentValidation;

namespace POC1.Application.Queries.ApiLogs
{
    public class GetApiLogsQueryValidator : AbstractValidator<GetApiLogsQuery>
    {
        public GetApiLogsQueryValidator()
        {
            RuleFor(x => x.From)
                .NotEmpty().WithMessage("From Date must not be empty");
            RuleFor(x => x.To)
                .NotEmpty()
                .WithMessage("To Date must not be empty"); ;
        }
    }
}
