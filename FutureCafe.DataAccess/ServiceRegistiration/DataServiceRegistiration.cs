using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Concrete.EntityFramework;
using FutureCafe.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FutureCafe.DataAccess.ServiceRegistiration
{
  public static class DataServiceRegistiration
  {
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConStr")));

      services.AddScoped<ICategoryDal, EFCategoryDal>();
      services.AddScoped<IPriceDal, EfPriceDal>();
      services.AddScoped<IProductDal, EfProductDal>();
      services.AddScoped<IStockDal, EfStockDal>();
      services.AddScoped<ICreditDal, EfCreditDal>();
      services.AddScoped<ISchoolClassDal, EfSchoolClassDal>();
      services.AddScoped<IStudentDal, EfStudentDal>();
      services.AddScoped<ITradeDal, EfTradeDal>();
      services.AddScoped<ITradeProductDal, EfTradeProductDal>();
      services.AddScoped<IProductCategoryDal, EfProductCategoryDal>();
      services.AddScoped<IProductPriceDal, EfProductPriceDal>();
      services.AddScoped<IStudentCreditDal, EfStudentCreditDal>();
      services.AddScoped<IUserDal, EfUserDal>();
      //services.AddTransient<DbContext, DatabaseContext>();
      return services;
    }
  }
}
