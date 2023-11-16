using FluentValidation;
using FutureCafe.Business.Constants;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
  public class SchoolClassValidator : AbstractValidator<SchoolClass>
  {
    public SchoolClassValidator()
    {
      RuleFor(x => x.Name).NotEmpty().WithName("Sınıf Adı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);
      RuleFor(x => x.Name).MaximumLength(100).WithName("Sınıf Adı").WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);
    }
  }
}
