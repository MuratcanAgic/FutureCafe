using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureCafe.DataAccess.EntityConfigurations
{
  public class CreditConfiguration : IEntityTypeConfiguration<Credit>
  {
    public void Configure(EntityTypeBuilder<Credit> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Amount).HasPrecision(8, 3).IsRequired();

      builder.HasData(
        new Credit { Id = 1, Amount = 100M },
        new Credit { Id = 2, Amount = 230M }
        );
    }
  }
}
