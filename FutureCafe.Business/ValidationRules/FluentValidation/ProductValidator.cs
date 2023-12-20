using FluentValidation;
using FutureCafe.Business.Constants;
using FutureCafe.Business.Dtos;
using FutureCafe.DataAccess.Abstract;

namespace FutureCafe.Business.ValidationRules.FluentValidation
{
  public class ProductValidator : AbstractValidator<ProductCreateEditDto>
  {
    IProductDal _productDal;

    public ProductValidator(IProductDal productDal)
    {
      _productDal = productDal;

      RuleFor(x => x.Name).NotEmpty().WithName("Ürün Adı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty)
        .MaximumLength(80).WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);

      RuleFor(x => x.Description).MaximumLength(150).WithName("Ürün Tanımı").WithMessage(x => "{PropertyName} " + Messages.StringMaxLength);

      RuleFor(x => x.ProductBarcodNo).NotEmpty().WithName("Ürün Barkod Numarası").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty)
        .MaximumLength(150).WithMessage(x => "{PropertyName} " + Messages.StringMaxLength)
        .Must(BarcodeNoUnique).WithMessage(Messages.ProductBarcodeDuplicate);

      RuleFor(x => x.SelectCategoryIds).NotNull().WithName("Kategori").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);

      RuleFor(x => x.SalePrice).GreaterThan(0).NotNull().WithName("Satış Fiyatı").WithMessage(x => "{PropertyName} " + Messages.CannotBeEmpty);

      RuleFor(x => x.ImageFile).SetValidator(new ImageFileValidator());


    }
    private bool BarcodeNoUnique(string barcodeNo)
    {
      return !_productDal.Any(a => a.ProductBarcodNo == barcodeNo);
    }
  }
}
