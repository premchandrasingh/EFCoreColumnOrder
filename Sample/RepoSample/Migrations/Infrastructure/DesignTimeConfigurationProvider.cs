using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace RepoSample.Migrations.Infrastructure
{
    internal class DesignTimeConfigurationProvider
    {
        private IConfiguration _configuration = null;

        public DesignTimeConfigurationProvider()
        {
            EnvironmentName = Environment.GetEnvironmentVariable(Constants.ASPNETCORE_ENVIRONMENT);
        }

        public string EnvironmentName { get; private set; }

        public IConfiguration Configuration
        {
            get
            {
                if (_configuration != null)
                    return _configuration;

                _configuration = new ConfigurationBuilder()
                .SetBasePath(GetCorrectBasePath())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .AddJsonFile($"appsettings.{EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

                return _configuration;
            }
        }
        private static string GetCorrectBasePath()
        {
            string basePath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Start looking for 'application.json' @ {basePath}");

            // Basic logic:- check "appsettings.json" exist at the path 
            Func<string, bool> isAppsettingJsonExist = (_path) =>
            {
                return File.Exists(Path.Combine(_path, "appsettings.json"));
            };

            // Case 1) When Api or Web is set as "Start up project" and try migration with nuget commad "Add-Migration Migration_Initial"
            // illustration basePath:- Cision.Web.Api
            string path = basePath;
            if (isAppsettingJsonExist(path))
            {
                // Case 1.a) check if it is in development or debug mode, if yes move up to project root
                // illustration basePath:- Cision.Web.Api\bin\debug\dotnetcore2.2
                if (path.IndexOf("bin\\debug", StringComparison.InvariantCultureIgnoreCase) > 0)
                {
                    path = Path.GetFullPath(Path.Combine(basePath, "..\\..\\.."));
                }
                return path;
            }

            throw new Exception("Path not found where application.json supposed to be. Try by setting the web or api project as 'start up'");
        }
    }
}
