using FluentValidation;
using OrderingService.Domain;

namespace OrderingService.Web.Code.Validators
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.EmployeeId).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Address).NotEmpty().MaximumLength(40);
            RuleFor(x => x.BriefTask).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ServiceDetails).MaximumLength(255);
            RuleFor(x => x.ContactPhone)
                .Matches(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")
                .NotEmpty().MaximumLength(20);
        }
    }
}
