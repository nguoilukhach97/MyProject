using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(p => p.Note).HasMaxLength(250);
            builder.Property(x => x.ReasonCancelation).HasMaxLength(250);
            builder.Property(x => x.UserCancel).IsRequired(false);

            builder.Property(x => x.Status).HasDefaultValueSql("0");
        }
    }
}
