using FluentValidation;
using OrderingService.Domain;

namespace OrderingService.Web.Code.Validators
{
    public class ReviewCreateDtoValidator : AbstractValidator<ReviewCreateDto>
    {
        public ReviewCreateDtoValidator()
        {
            RuleFor(x => x.EmployeeId).NotEmpty();
            RuleFor(x => x.Text).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Rate).NotEmpty().InclusiveBetween(0, 5);
        }
    }
}
