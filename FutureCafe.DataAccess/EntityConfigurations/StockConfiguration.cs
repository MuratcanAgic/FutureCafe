using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureCafe.DataAccess.EntityConfigurations
{
  public class StockConfiguration : IEntityTypeConfiguration<Stock>

  {
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.ProductCount).IsRequired();
      builder.Property(x => x.ProductId).IsRequired();
    }
  }
}
