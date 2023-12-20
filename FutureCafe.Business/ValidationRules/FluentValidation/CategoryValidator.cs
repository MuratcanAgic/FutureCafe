using FluentValidation;
using FutureCafe.Business.Constants;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
  public class CategoryValidator : AbstractValidator<Category>
  {
    ICategoryDal _categoryDal;

    public CategoryValidator(ICategoryDal categoryDal)
    {
      _categoryDal = categoryDal;
      RuleFor(x => x.Name).NotEmpty().WithName("Kategori Adı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty)
        .MaximumLength(100).WithMessage(x => "{PropertyName} " + Messages.StringMaxLength)
        .Must(CategoryUnique).WithMessage(Messages.CategoryNameDuplicate);
    }

    private bool CategoryUnique(string name)
    {
      return !_categoryDal.Any(a => a.Name == name);
    }
  }
}
