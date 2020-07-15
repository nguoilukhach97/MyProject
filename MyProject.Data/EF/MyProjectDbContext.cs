using Microsoft.EntityFrameworkCore;
using MyProject.Data.Configurations;
using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyProject.Data.EF
{
    public class MyProjectDbContext : DbContext
    {
        public MyProjectDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.ApplyConfiguration(new BrandConfiguration());
            builder.ApplyConfiguration(new EvaluteConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductDetailConfiguration());
            builder.ApplyConfiguration(new ProductImageConfiguration());
            builder.ApplyConfiguration(new ProductInOrderConfiguration());
            builder.ApplyConfiguration(new ProductInCategoryConfiguration());

            //base.OnModelCreating(builder);
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Evalute> Evalutes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductInCategory> ProductInCategories { get; set; }
        public DbSet<ProductInOrder> productInOrders { get; set; }
    }
}
