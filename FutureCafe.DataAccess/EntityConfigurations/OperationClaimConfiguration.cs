using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureCafe.DataAccess.EntityConfigurations
{
  public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
  {
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

      builder.HasData(
         new OperationClaim { Id = 1, Name = "Admin" },
         new OperationClaim { Id = 2, Name = "CanteenKeeper" }
        );
    }
  }
}
