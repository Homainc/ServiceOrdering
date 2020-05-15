using FluentValidation;
using OrderingService.Domain;

namespace OrderingService.Web.Code.Validators
{
    public class ReviewDtoValidator : AbstractValidator<ReviewDto>
    {
        public ReviewDtoValidator()
        {
            RuleSet("Create", () =>
            {
                RuleFor(x => x.EmployeeId).NotNull();
                RuleFor(x => x.Text).NotEmpty().MaximumLength(255);
                RuleFor(x => x.Rate).NotNull().InclusiveBetween(0, 5);
            });
            RuleSet("Id", () => { RuleFor(x => x.Id).NotNull(); });
        }
    }
}
