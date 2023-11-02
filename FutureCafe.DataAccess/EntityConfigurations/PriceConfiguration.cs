using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureCafe.DataAccess.EntityConfigurations
{
  public class PriceConfiguration : IEntityTypeConfiguration<Price>
  {
    public void Configure(EntityTypeBuilder<Price> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.BuyingPrice).HasPrecision(8, 3);
      builder.Property(x => x.SalePrice).IsRequired().HasPrecision(8, 3);

      builder.HasData(
        new Price { Id = 1, BuyingPrice = 15.2M, SalePrice = 17.5M },
        new Price { Id = 2, BuyingPrice = 60M, SalePrice = 86.7M }
        );
    }
  }
}
