using FluentValidation;
using FutureCafe.Business.Constants;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
  public class SchoolClassValidator : AbstractValidator<SchoolClass>
  {
    ISchoolClassDal _schoolClassDal;

    public SchoolClassValidator(ISchoolClassDal schoolClassDal)
    {
      _schoolClassDal = schoolClassDal;
      RuleFor(x => x.Name).NotEmpty().WithName("Sınıf Adı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty)
        .MaximumLength(5).WithMessage(x => "{PropertyName} " + Messages.StringMaxLength)
        .Must(SchoolClassUnique).WithMessage(Messages.SchoolClassNameDuplicate);

    }
    private bool SchoolClassUnique(string name)
    {
      return !_schoolClassDal.Any(a => a.Name == name);
    }
  }
}
