using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");

            builder.Property(p => p.FirstName).HasMaxLength(200).IsRequired();

            builder.Property(p => p.LastName).HasMaxLength(200).IsRequired();

            builder.Property(p=>p.Dob).HasColumnType("datetime");
        }
    }
}
