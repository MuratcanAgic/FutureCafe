using FluentValidation;
using FutureCafe.Business.Abstract;
using FutureCafe.Business.Concrete;
using FutureCafe.Business.Dtos;
using FutureCafe.Business.Mapping;
using FutureCafe.Business.ValidationRules.FluentValidation;
using FutureCafe.Entities.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace FutureCafe.Business.ServiceRegistiration
{
  public static class BusinessServiceRegistration
  {
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
      //services
      services.AddScoped<ICategoryService, CategoryService>();
      services.AddScoped<IProductService, ProductService>();
      services.AddScoped<ISchoolClassService, SchoolClassService>();
      services.AddScoped<IStudentService, StudentService>();
      services.AddScoped<ITradeService, TradeService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IAuthService, AuthService>();
      services.AddScoped<IOperationClaimService, OperationClaimService>();
      //validators
      services.AddScoped<IValidator<Category>, CategoryValidator>();
      services.AddScoped<IValidator<ProductCreateEditDto>, ProductValidator>();
      services.AddScoped<IValidator<SchoolClass>, SchoolClassValidator>();
      services.AddScoped<IValidator<Student>, StudentValidator>();
      services.AddScoped<IValidator<UserForRegisterDto>, UserValidator>();

      services.AddAutoMapper(typeof(MappingProfile));

      return services;
    }
  }
}
