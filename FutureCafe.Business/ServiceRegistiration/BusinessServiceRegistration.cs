using FluentValidation;
using FutureCafe.Business.Abstract;
using FutureCafe.Business.Concrete;
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
      //validators
      services.AddScoped<IValidator<Category>, CategoryValidator>();
      services.AddScoped<IValidator<Product>, ProductValidator>();
      services.AddScoped<IValidator<SchoolClass>, SchoolClassValidator>();
      services.AddScoped<IValidator<Student>, StudentValidator>();

      services.AddAutoMapper(typeof(MappingProfile));

      return services;
    }
  }
}
