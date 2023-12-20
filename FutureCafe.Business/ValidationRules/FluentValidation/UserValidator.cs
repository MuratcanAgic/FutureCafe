using FluentValidation;
using FutureCafe.Business.Constants;
using FutureCafe.Business.Dtos;
using FutureCafe.DataAccess.Abstract;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
  public class UserValidator : AbstractValidator<UserForRegisterDto>
  {
    IUserDal _userDal;

    public UserValidator(IUserDal userDal)
    {
      _userDal = userDal;

      RuleFor(x => x.FirstName).NotEmpty().WithName("İsim").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty)
                    .MaximumLength(50).WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);


      RuleFor(x => x.LastName).NotEmpty().WithName("Soyisim").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty)
                    .MaximumLength(50).WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);


      RuleFor(x => x.Email).NotEmpty().WithName("Kullanıcı Adı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty)
                    .MaximumLength(50).WithMessage(x => "{PropertyName} " + Messages.StringMaxLength)
                    .Must(UserUnique).WithMessage(Messages.UserEmailDuplicate);


      RuleFor(p => p.Password).NotEmpty().WithName("Şifre").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty)
                    .MinimumLength(8).WithMessage(x => "{PropertyName} " + Messages.StringMinLength)
                    .MaximumLength(16).WithMessage(x => "{PropertyName} " + Messages.StringMaxLength)
                    .Matches(@"[A-Z]+").WithMessage(x => "{PropertyName} " + Messages.AtLeastOneUpperCaseLetter)
                    .Matches(@"[a-z]+").WithMessage(x => "{PropertyName} " + Messages.AtLeastOneLowerCaseLetter)
                    .Matches(@"[0-9]+").WithMessage(x => "{PropertyName} " + Messages.AtLeastOneNumber)
                    .Matches(@"[\!\?\*\.]+").WithMessage(x => "{PropertyName} " + Messages.AtLeastOneCharacterLetter)
                    .Equal(p => p.PasswordConfirm).WithMessage(x => "{PropertyName} " + Messages.PasswordConfirmMatch);

      RuleFor(p => p.PasswordConfirm).NotEmpty().WithName("Şifre Tekrar").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty)
                    .MinimumLength(8).WithMessage(x => "{PropertyName} " + Messages.StringMinLength)
                    .MaximumLength(16).WithMessage(x => "{PropertyName} " + Messages.StringMaxLength)
                    .Matches(@"[A-Z]+").WithMessage(x => "{PropertyName} " + Messages.AtLeastOneUpperCaseLetter)
                    .Matches(@"[a-z]+").WithMessage(x => "{PropertyName} " + Messages.AtLeastOneLowerCaseLetter)
                    .Matches(@"[0-9]+").WithMessage(x => "{PropertyName} " + Messages.AtLeastOneNumber)
                    .Matches(@"[\!\?\*\.]+").WithMessage(x => "{PropertyName} " + Messages.AtLeastOneCharacterLetter);

      RuleFor(x => x.SelectClaimIds).NotNull().WithName("Roller").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);

    }

    private bool UserUnique(string email)
    {
      return !_userDal.Any(a => a.Email == email);
    }
  }
}
