using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace RepoSample.Migrations.Infrastructure
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> :
         IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {

        public TContext CreateDbContext(string[] args)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}

            DesignTimeConfigurationProvider configProvider = new DesignTimeConfigurationProvider();
            var connectionString = configProvider.Configuration.GetConnectionString(Constants.CONNECTION_NAME);
            
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{Constants.CONNECTION_NAME}' is null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return CreateNewInstance(optionsBuilder.Options);

        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);
       
    }
}
