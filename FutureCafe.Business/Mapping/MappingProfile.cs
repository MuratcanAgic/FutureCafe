using AutoMapper;
using FutureCafe.Business.Dtos;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.Mapping
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<SchoolClass, SchoolClassCreateEditDto>().ReverseMap();
      CreateMap<SchoolClass, SchoolClassViewDto>().ReverseMap();

      CreateMap<Category, CategoryCreateEditDto>().ReverseMap();
      CreateMap<Category, CategoryViewDto>().ReverseMap();

      CreateMap<Student, StudentViewDto>().ReverseMap();
      CreateMap<Student, StudentCreateEditDto>().ReverseMap();
      CreateMap<Student, StudentDetailDto>().ReverseMap();
      CreateMap<Student, StudentCreditReportDto>().ReverseMap();

      CreateMap<Product, ProductCreateEditDto>().ReverseMap()
        .ForSourceMember(x => x.SelectCategoryIds, opt => opt.DoNotValidate())
        .ForSourceMember(x => x.BuyingPrice, opt => opt.DoNotValidate())
        .ForSourceMember(x => x.SalePrice, opt => opt.DoNotValidate())
        ;

      CreateMap<Product, ProductViewDto>().ReverseMap();
      CreateMap<Product, ProductDetailDto>().ReverseMap();
      CreateMap<Product, ProductPriceReportDto>().ReverseMap();

      CreateMap<User, UserForViewDto>().ReverseMap();
      CreateMap<User, UserForRegisterDto>().ReverseMap()
        .ForSourceMember(x => x.PasswordConfirm, opt => opt.DoNotValidate())
        .ForSourceMember(x => x.SelectClaimIds, opt => opt.DoNotValidate());

      CreateMap<User, UserForLoginDto>().ReverseMap();

      CreateMap<Trade, TradeReportDto>().ReverseMap();
      CreateMap<TradeProduct, TradeProductReportDto>().ReverseMap();


    }
  }
}
