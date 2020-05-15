using FluentValidation;
using OrderingService.Domain;

namespace OrderingService.Web.Code.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleSet("Create", () =>
            {
                RuleFor(x => x.ClientId).NotNull();
                RuleFor(x => x.EmployeeId).NotNull();
                RuleFor(x => x.Price).GreaterThan(0);
                RuleFor(x => x.Date).NotEmpty();
                RuleFor(x => x.Address).NotEmpty().MaximumLength(40);
                RuleFor(x => x.BriefTask).NotEmpty().MaximumLength(50);
                RuleFor(x => x.ServiceDetails).MaximumLength(255);
                RuleFor(x => x.ContactPhone).NotEmpty().MaximumLength(20);
            });
            RuleSet("Id", () => { RuleFor(x => x.Id).NotNull(); });
        }
    }
}
