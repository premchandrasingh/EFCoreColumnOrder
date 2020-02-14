using Microsoft.EntityFrameworkCore;

namespace RepoSample.Migrations.Infrastructure
{
    /// <summary>
    /// WARNING(PREM):
    /// Even though this factory class is not reference from anywhere or by any runtime part of the application. 
    /// It is BEING USED by MIGRATION(ONLY) when firing nuget command Add-Migration <migration name>. Removing this class will result in failure to migration.
    /// For more detail go to parent class <see cref="DesignTimeDbContextFactoryBase{TContext}"/> and read description of <see cref="Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory{TContext}"/>
    /// </summary>
    public class AppDbContextFactory : DesignTimeDbContextFactoryBase<AppDbContext>
    {
        
        protected override AppDbContext CreateNewInstance(DbContextOptions<AppDbContext> options)
        {
            //// https://www.koskila.net/visual-studio-debugger-instance-from-code/
            //// Launching a new debugger instance from code in Visual Studio
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}

            return new AppDbContext(options);
        }
    }
}
