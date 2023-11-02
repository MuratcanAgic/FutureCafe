using FluentValidation;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
  public class CategoryValidator : AbstractValidator<Category>
  {
    public CategoryValidator()
    {
    }
  }
}
