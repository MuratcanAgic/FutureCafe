using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureCafe.DataAccess.EntityConfigurations
{
  public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
  {
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
      builder.HasKey(x => x.Id);
      builder.HasData(
        new ProductCategory { Id = 1, ProductId = 1, CategoryId = 1 },
        new ProductCategory { Id = 2, ProductId = 2, CategoryId = 3 },
        new ProductCategory { Id = 3, ProductId = 2, CategoryId = 4 }
        );
    }
  }
}
