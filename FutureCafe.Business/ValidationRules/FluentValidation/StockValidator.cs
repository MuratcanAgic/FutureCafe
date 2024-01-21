using FluentValidation;
using FutureCafe.Business.Constants;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
  public class StockValidator : AbstractValidator<Stock>
  {
    public StockValidator()
    {
      RuleFor(x => x.ProductId).GreaterThan(0).WithName("Ürün").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);
      RuleFor(x => x.ProductCount).GreaterThan(0).WithName("Ürün Sayısı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);
    }

  }
}
