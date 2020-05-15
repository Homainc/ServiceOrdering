using FluentValidation;
using OrderingService.Domain;

namespace OrderingService.Web.Code.Validators
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty()
                .Matches(@"(?=.*[a - z])(?=.*[A - Z])(?=.*\d)(?=.*[@$.,/\\!%*?&()<>=_+])[A-Za-z\d@$.,/\\!%*?&()<>=_+]{8,}");
            RuleFor(x => x.PhoneNumber).Matches(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$");
            RuleFor(x => x.FirstName).MaximumLength(30);
            RuleFor(x => x.LastName).MaximumLength(30);
        }
    }

    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);
            RuleFor(x => x.PhoneNumber).Matches(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$");
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.FirstName).MaximumLength(30);
            RuleFor(x => x.LastName).MaximumLength(30);
        }
    }
}
