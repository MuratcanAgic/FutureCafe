using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureCafe.DataAccess.EntityConfigurations
{
  public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
  {
    public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
    {
      builder.HasKey(x => x.Id);

      builder.HasData(
        new UserOperationClaim { Id = 1, UserId = 1, OperationClaimId = 1 },
        new UserOperationClaim { Id = 2, UserId = 2, OperationClaimId = 2 }
       );
    }
  }
}
