using FluentValidation;
using FutureCafe.Business.Constants;
using Microsoft.AspNetCore.Http;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
  public class ImageFileValidator : AbstractValidator<IFormFile>
  {
    public ImageFileValidator()
    {
      RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(40000000)
          .WithMessage(Messages.FileSizeTooBig);

      RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
          .WithMessage(Messages.FileMustBeImage);
    }
  }
}
