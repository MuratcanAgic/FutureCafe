using FluentValidation;
using FutureCafe.Business.Constants;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
  public class StudentValidator : AbstractValidator<Student>
  {
    public StudentValidator()
    {
      RuleFor(x => x.Name).NotEmpty().WithName("Öğrenci Adı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);
      RuleFor(x => x.Name).MaximumLength(50).WithName("Öğrenci Adı").WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);

      RuleFor(x => x.SurName).NotEmpty().WithName("Öğrenci Soyadı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);
      RuleFor(x => x.SurName).MaximumLength(50).WithName("Öğrenci Soyadı").WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);

      RuleFor(x => x.ImageUrl).MaximumLength(500).WithName("Öğrenci Fotoğraf Yolu").WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);

      RuleFor(x => x.CardNumber).NotEmpty().WithName("Öğrenci Kart Numarası").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);
      RuleFor(x => x.CardNumber).MaximumLength(150).WithName("Öğrenci Kart Numarası").WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);

      RuleFor(x => x.StudentSchoolNumber).MaximumLength(80).WithName("Öğrenci Okul Numarası").WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);

      RuleFor(x => x.SchoolClassId).GreaterThan(0).WithName("Öğrenci Sınıfı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);
    }
  }
}
