using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(p => p.Name).HasMaxLength(250);
            builder.Property(p => p.Description).HasColumnType("ntext");
            builder.Property(x => x.Details).HasColumnType("ntext");
            builder.Property(p => p.ViewCount).HasDefaultValueSql("0");

            builder.HasOne(p => p.Brand).WithMany(p => p.Products).HasForeignKey(p => p.BrandId);
            

            
        }
    }
}
