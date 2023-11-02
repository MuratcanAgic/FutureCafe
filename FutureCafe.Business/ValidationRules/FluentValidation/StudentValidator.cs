using FluentValidation;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
  public class StudentValidator : AbstractValidator<Student>
  {
    public StudentValidator()
    {

    }
  }
}
