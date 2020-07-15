using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyProject.Data.EF
{
    public class MyProjectDbFactory : IDesignTimeDbContextFactory<MyProjectDbContext>
    {
        public MyProjectDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            var connectionString = configuration.GetConnectionString("myProjectDb");
            var optionsBuilder = new DbContextOptionsBuilder<MyProjectDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new MyProjectDbContext(optionsBuilder.Options);
        }
    }
}
