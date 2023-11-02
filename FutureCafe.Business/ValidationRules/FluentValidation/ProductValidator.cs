using FluentValidation;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
  public class ProductValidator : AbstractValidator<Product>
  {
    public ProductValidator()
    {

    }
  }
}
