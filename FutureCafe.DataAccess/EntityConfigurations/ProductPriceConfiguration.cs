using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureCafe.DataAccess.EntityConfigurations
{
  public class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPrice>
  {
    public void Configure(EntityTypeBuilder<ProductPrice> builder)
    {
      builder.HasKey(x => x.Id);
      builder.HasData(
         new ProductPrice { Id = 1, PriceId = 1, ProductId = 1 },
         new ProductPrice { Id = 2, PriceId = 2, ProductId = 2 }
         );
    }
  }
}
