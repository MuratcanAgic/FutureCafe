using FluentValidation;
using FutureCafe.Business.Constants;
using FutureCafe.Business.Dtos;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
	public class ProductValidator : AbstractValidator<ProductCreateEditDto>
	{
		public ProductValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithName("Ürün Adı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);
			RuleFor(x => x.Name).MaximumLength(80).WithName("Ürün Adı").WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);

			RuleFor(x => x.Description).MaximumLength(150).WithName("Ürün Tanımı").WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);

			RuleFor(x => x.ProductBarcodNo).NotEmpty().WithName("Ürün Barkod Numarası").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);
			RuleFor(x => x.ProductBarcodNo).MaximumLength(150).WithName("Ürün Barkod Numarası").WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);

			RuleFor(x => x.SelectCategoryIds).NotNull().WithName("Kategori").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);

			RuleFor(x => x.SalePrice).GreaterThan(0).NotNull().WithName("Satış Fiyatı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);
		}
	}
}
