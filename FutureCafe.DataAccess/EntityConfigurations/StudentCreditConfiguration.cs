using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureCafe.DataAccess.EntityConfigurations
{
  public class StudentCreditConfiguration : IEntityTypeConfiguration<StudentCredit>
  {
    public void Configure(EntityTypeBuilder<StudentCredit> builder)
    {
      builder.HasKey(x => x.Id);
      builder.HasData(
        new StudentCredit { Id = 1, StudentId = 1, CreditId = 1 },
        new StudentCredit { Id = 2, StudentId = 2, CreditId = 2 }
        );
    }
  }
}
