using FluentValidation;
using OrderingService.Domain;

namespace OrderingService.Web.Code.Validators
{
    public class ReviewDtoValidator : AbstractValidator<ReviewDTO>
    {
        public ReviewDtoValidator()
        {
            RuleSet("Create", () =>
            {
                RuleFor(x => x.ClientId).NotNull();
                RuleFor(x => x.EmployeeId).NotNull();
                RuleFor(x => x.Text).NotEmpty().MaximumLength(255);
            });
            RuleSet("Id", () => { RuleFor(x => x.Id).NotNull(); });
        }
    }
}
