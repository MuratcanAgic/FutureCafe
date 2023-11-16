using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureCafe.DataAccess.EntityConfigurations
{
  public class CategoryConfiguration : IEntityTypeConfiguration<Category>
  {
    public void Configure(EntityTypeBuilder<Category> builder)
    {
      builder.HasKey(c => c.Id);
      builder.Property(c => c.Name).IsRequired().HasMaxLength(80);

      builder
            .HasMany(c => c.ProductCategory)
            .WithOne(pc => pc.Category)
            .HasForeignKey(pc => pc.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

      builder.HasData(
        new Category { Id = 1, Name = "Gazlı içeçek" },
        new Category { Id = 2, Name = "Çikolata" },
        new Category { Id = 3, Name = "Kuruyemiş" },
        new Category { Id = 4, Name = "Şakuteri" }
      );
    }
  }
}
