using FutureCafe.Core.Utilities.Security;
using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureCafe.DataAccess.EntityConfigurations
{
  public class UserConfiguration : IEntityTypeConfiguration<User>
  {

    public void Configure(EntityTypeBuilder<User> builder)
    {
      byte[] passwordHashAdmin, passwordSaltAdmin, passwordHashCanteenKeeper, passwordSaltCanteenKeeper;
      string passwordAdmin = "1234";
      string passwordCanteenKeeper = "qwerty";
      HashingHelper.CreatePasswordHash(passwordAdmin, out passwordHashAdmin, out passwordSaltAdmin);
      HashingHelper.CreatePasswordHash(passwordCanteenKeeper, out passwordHashCanteenKeeper, out passwordSaltCanteenKeeper);

      builder.HasKey(x => x.Id);
      builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
      builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
      builder.Property(x => x.Email).IsRequired().HasMaxLength(50);

      builder.HasData(
        new User
        {
          Id = 1,
          FirstName = "Muratcan",
          LastName = "Agiç",
          Email = "admin@futurecafe.com",
          PasswordHash = passwordHashAdmin,
          PasswordSalt = passwordSaltAdmin,
          Status = true,
        },
        new User
        {
          Id = 2,
          FirstName = "Anıl",
          LastName = "Aktepe",
          Email = "canteen@futurecafe.com",
          PasswordHash = passwordHashCanteenKeeper,
          PasswordSalt = passwordSaltCanteenKeeper,
          Status = true,
        }
        );
    }
  }
}
