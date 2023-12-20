using FluentValidation;
using FutureCafe.Business.Constants;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
  public class StudentValidator : AbstractValidator<Student>
  {
    IStudentDal _studentDal;

    public StudentValidator(IStudentDal studentDal)
    {
      _studentDal = studentDal;

      RuleFor(x => x.Name).NotEmpty().WithName("Öğrenci Adı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty)
        .MaximumLength(50).WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);

      RuleFor(x => x.SurName).NotEmpty().WithName("Öğrenci Soyadı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty)
        .MaximumLength(50).WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);


      RuleFor(x => x.ImageUrl).MaximumLength(500).WithName("Öğrenci Fotoğraf Yolu").WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);

      RuleFor(x => x.CardNumber).NotEmpty().WithName("Öğrenci Kart Numarası").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty)
        .MaximumLength(150).WithMessage(x => "{PropertyName} " + Messages.StringMaxLength)
        .Must(StudentUnique).WithMessage(Messages.StudentCardNoDuplicate);

      RuleFor(x => x.StudentSchoolNumber).MaximumLength(80).WithName("Öğrenci Okul Numarası").WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);

      RuleFor(x => x.SchoolClassId).GreaterThan(0).WithName("Öğrenci Sınıfı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);

    }

    private bool StudentUnique(string cardNo)
    {
      return !_studentDal.Any(a => a.CardNumber == cardNo);
    }
  }
}
