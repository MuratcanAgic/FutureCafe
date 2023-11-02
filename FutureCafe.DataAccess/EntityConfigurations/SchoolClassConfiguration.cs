using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureCafe.DataAccess.EntityConfigurations
{
  public class SchoolClassConfiguration : IEntityTypeConfiguration<SchoolClass>
  {
    public void Configure(EntityTypeBuilder<SchoolClass> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

      builder.HasData(
        new SchoolClass { Id = 1, Name = "A" },
        new SchoolClass { Id = 2, Name = "B" },
        new SchoolClass { Id = 3, Name = "101" },
        new SchoolClass { Id = 4, Name = "102" }

      );
    }
  }
}
