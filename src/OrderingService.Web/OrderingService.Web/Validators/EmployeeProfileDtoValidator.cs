using FluentValidation;
using OrderingService.Domain;

namespace OrderingService.Web.Validators
{
    public class EmployeeProfileDtoValidator : AbstractValidator<EmployeeProfileDTO>
    {
        public EmployeeProfileDtoValidator()
        {
            RuleSet("Create", () =>
            {
                RuleFor(x => x.ServiceCost).GreaterThan(0);
                RuleFor(x => x.ServiceType).NotNull();
                RuleFor(x => x.Description).MaximumLength(255);
                RuleFor(x => x.ServiceType).NotEmpty().MaximumLength(20);
            });
            RuleSet("Id", () => { RuleFor(x => x.Id).NotNull(); });
        }
    }
}
