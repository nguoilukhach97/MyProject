using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Configurations
{
    public class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetail>
    {
        public void Configure(EntityTypeBuilder<ProductDetail> builder)
        {
            builder.ToTable("ProductDetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            
            builder.Property(x => x.Price).HasDefaultValueSql("0");
            builder.Property(x => x.PromotionPrice).HasDefaultValueSql("0");
            builder.Property(x => x.Quantity).HasDefaultValueSql("0");
            builder.Property(x => x.Warranty).HasDefaultValueSql("0");
            builder.Property(x => x.Size).HasDefaultValueSql("0");
            builder.Property(x => x.Status).HasDefaultValueSql("1");

            builder.Property(x => x.DateCreated).HasColumnType("datetime");
            builder.Property(x => x.DateModified).HasColumnType("datetime");
            builder.Property(x => x.DateModified).IsRequired(false);
            builder.Property(x => x.UserCreated).HasMaxLength(100).IsUnicode(false);
            builder.Property(x => x.UserModified).HasMaxLength(100).IsUnicode(false);

            builder.HasOne(p => p.Product).WithMany(p => p.ProductDetails).HasForeignKey(p => p.ProductId);
        }
    }
}
