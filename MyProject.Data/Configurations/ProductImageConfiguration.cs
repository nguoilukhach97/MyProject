using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.ImagePath).HasMaxLength(250);
            builder.Property(p => p.Caption).HasColumnType("ntext");
            builder.Property(p => p.SortOrder).IsRequired(false);
            builder.Property(p => p.Status).HasDefaultValueSql("1");

            builder.HasOne(p => p.Product).WithMany(p => p.ProductImages).HasForeignKey(p=>p.ProductId);
        }
    }
}
