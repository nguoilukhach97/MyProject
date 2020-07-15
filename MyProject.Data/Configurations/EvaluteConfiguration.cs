using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Configurations
{
    public class EvaluteConfiguration : IEntityTypeConfiguration<Evalute>
    {
        public void Configure(EntityTypeBuilder<Evalute> builder)
        {
            builder.ToTable("Evalutes");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.CustomerName).HasMaxLength(250);
            builder.Property(p=>p.PhoneNumber).HasMaxLength(12);
            builder.Property(p => p.PhoneNumber).IsUnicode(false);
            builder.Property(p => p.Rate).HasDefaultValueSql("5");
            builder.Property(p => p.ContentEvalute).HasColumnType("ntext");
            builder.Property(p => p.Status).HasDefaultValueSql("1");

            builder.HasOne(x => x.Product).WithMany(x => x.Evalutes).HasForeignKey(p => p.ProductId);
        }
    }
}
