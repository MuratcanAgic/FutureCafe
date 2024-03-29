﻿using FutureCafe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureCafe.DataAccess.EntityConfigurations
{
  public class TradeConfiguration : IEntityTypeConfiguration<Trade>
  {
    public void Configure(EntityTypeBuilder<Trade> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.StudentId).IsRequired();

    }
  }
}
