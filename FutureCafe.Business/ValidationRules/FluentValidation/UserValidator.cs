using FluentValidation;
using FutureCafe.Business.Constants;
using FutureCafe.Business.Dtos;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
	public class UserValidator : AbstractValidator<UserForRegisterDto>
	{
		public UserValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().WithName("İsim").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty)
										.MaximumLength(50).WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);


			RuleFor(x => x.LastName).NotEmpty().WithName("Soyisim").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty)
										.MaximumLength(50).WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);


			RuleFor(x => x.Email).NotEmpty().WithName("Kullanıcı Adı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty)
										.MaximumLength(50).WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);


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
		}
	}
}
