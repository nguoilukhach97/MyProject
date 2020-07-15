using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Configurations
{
    public class ProductInOrderConfiguration : IEntityTypeConfiguration<ProductInOrder>
    {
        public void Configure(EntityTypeBuilder<ProductInOrder> builder)
        {
            builder.ToTable("ProductInOrder");
            builder.HasKey(p => new { p.ProductId, p.OrderId });
            builder.Property(x => x.Quantity).HasDefaultValueSql("1");

            builder.HasOne(x => x.ProductDetail).WithMany(x => x.ProductInOrders).HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.Order).WithMany(x => x.ProductInOrders).HasForeignKey(x => x.OrderId);
        }
    }
}
