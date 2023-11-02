using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FutureCafe.DataAccess.Context
{
  public class DatabaseContext : DbContext
  {
    public DatabaseContext()
    {

    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //apply entitiy configurations
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    //DbSets
    public DbSet<Category> Categories { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductPrice> ProductPrices { get; set; }
    public DbSet<Credit> Credits { get; set; }
    public DbSet<SchoolClass> SchoolClasses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentCategory> StudentCategories { get; set; }
    public DbSet<StudentCredit> StudentCredits { get; set; }
    public DbSet<Trade> Trades { get; set; }
    public DbSet<TradeProduct> TradeProducts { get; set; }
  }
}
