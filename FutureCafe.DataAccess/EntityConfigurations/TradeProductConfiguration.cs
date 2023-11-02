using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureCafe.DataAccess.EntityConfigurations
{
  public class TradeProductConfiguration : IEntityTypeConfiguration<TradeProduct>
  {
    public void Configure(EntityTypeBuilder<TradeProduct> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.ProductCount).IsRequired();
    }
  }
}
