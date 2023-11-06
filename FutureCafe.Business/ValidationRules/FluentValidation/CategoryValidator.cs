using FluentValidation;
using FutureCafe.Business.Constants;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
  public class CategoryValidator : AbstractValidator<Category>
  {
    public CategoryValidator()
    {
      RuleFor(x => x.Name).NotEmpty().WithName("Kategori").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);
      RuleFor(x => x.Name).MaximumLength(100).WithName("Kategori").WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);
    }
  }
}
