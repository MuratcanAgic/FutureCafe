using FluentValidation;
using FutureCafe.Business.Constants;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
  public class UserValidator : AbstractValidator<User>
  {
    public UserValidator()
    {
      RuleFor(x => x.FirstName).NotEmpty().WithName("İsim").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);
      RuleFor(x => x.FirstName).MaximumLength(50).WithName("İsim").WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);

      RuleFor(x => x.LastName).NotEmpty().WithName("Soyisim").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);
      RuleFor(x => x.LastName).MaximumLength(50).WithName("Soyisim").WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);
    }
  }
}
