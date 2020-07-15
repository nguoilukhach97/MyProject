using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(250);
            builder.Property(p => p.Image).HasMaxLength(250);
            builder.Property(p => p.Status).HasDefaultValueSql("1");

        }
    }
}
