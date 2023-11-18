using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureCafe.DataAccess.EntityConfigurations
{
  public class StudentConfiguration : IEntityTypeConfiguration<Student>
  {
    public void Configure(EntityTypeBuilder<Student> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
      builder.Property(x => x.SurName).IsRequired().HasMaxLength(50);
      builder.Property(x => x.ImageUrl).HasMaxLength(500);
      builder.Property(x => x.CardNumber).IsRequired().HasMaxLength(150);
      builder.Property(x => x.StudentSchoolNumber).HasMaxLength(80);
      builder.Property(x => x.SchoolClassId).IsRequired();

      builder.HasOne(x => x.SchoolClass)
                .WithMany(x => x.Students)
                .HasForeignKey(x => x.SchoolClassId)
                .OnDelete(DeleteBehavior.Restrict);

      builder.HasData(
        new Student { Id = 1, Name = "Fatmanur", SurName = "Agiç", CardNumber = "50467084778", SchoolClassId = 1, StudentSchoolNumber = "3001" },
        new Student { Id = 2, Name = "Muratcan", SurName = "Agiç", CardNumber = "511750565", SchoolClassId = 1, StudentSchoolNumber = "2894" }
      );
    }
  }
}
