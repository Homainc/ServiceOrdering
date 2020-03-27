using FluentValidation;
using OrderingService.Domain;

namespace OrderingService.Web.Code.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDTO>
    {
        public UserDtoValidator()
        {
            RuleSet("SignUp", () =>
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.FirstName).NotEmpty().MaximumLength(20);
                RuleFor(x => x.LastName).NotEmpty().MaximumLength(20);
                RuleFor(x => x.Password).NotEmpty();
            });
            RuleSet("LogIn", () =>
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            });
            RuleSet("Id", () => { RuleFor(x => x.Id).NotNull(); });
        }
    }
}
