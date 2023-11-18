using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureCafe.DataAccess.EntityConfigurations
{
  public class ProductConfiguration : IEntityTypeConfiguration<Product>
  {
    public void Configure(EntityTypeBuilder<Product> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Name).IsRequired().HasMaxLength(80);
      builder.Property(x => x.Description).HasMaxLength(150);
      builder.Property(x => x.ProductBarcodNo).IsRequired().HasMaxLength(150);

      /*   builder
             .HasMany(p => p.ProductCategory)
             .WithOne(pc => pc.Product)
             .HasForeignKey(pc => pc.ProductId)
             .OnDelete(DeleteBehavior.Restrict);*/

      builder.HasData(
       new Product { Id = 1, Name = "Beypazarı Maden Suyu", ProductBarcodNo = "8691381000486" },
       new Product { Id = 2, Name = "Ahir Tulum Peyniri", ProductBarcodNo = "8699118050490" }
     );
    }
  }
}
