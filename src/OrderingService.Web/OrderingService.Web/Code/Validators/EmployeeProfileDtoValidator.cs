using FluentValidation;
using OrderingService.Domain;

namespace OrderingService.Web.Code.Validators
{
    public class EmployeeProfileCreateDtoValidator : AbstractValidator<EmployeeProfileCreateDto>
    {
        public EmployeeProfileCreateDtoValidator()
        {
            RuleFor(x => x.ServiceCost).GreaterThan(0);
            RuleFor(x => x.Description).MaximumLength(255);
            RuleFor(x => x.ServiceType).NotEmpty().MaximumLength(20);
            RuleFor(x => x.UserId).NotEmpty();
        }
    }

    public class EmployeeProfileUpdateDtoValidator : AbstractValidator<EmployeeProfileUpdateDto>
    {
        public EmployeeProfileUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.ServiceCost).GreaterThan(0);
            RuleFor(x => x.Description).MaximumLength(255);
            RuleFor(x => x.ServiceType).NotEmpty().MaximumLength(20);
        }
    }
}
