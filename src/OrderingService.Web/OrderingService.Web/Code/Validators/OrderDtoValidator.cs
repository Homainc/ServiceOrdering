using FluentValidation;
using OrderingService.Domain;

namespace OrderingService.Web.Code.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDTO>
    {
        public OrderDtoValidator()
        {
            RuleSet("Create", () =>
            {
                RuleFor(x => x.ClientId).NotNull();
                RuleFor(x => x.EmployeeId).NotNull();
                RuleFor(x => x.Price).GreaterThan(0);
                RuleFor(x => x.IsClosed).NotEqual(true);
            });
            RuleSet("Id", () => { RuleFor(x => x.Id).NotNull(); });
        }
    }
}
