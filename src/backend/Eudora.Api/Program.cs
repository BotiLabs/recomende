using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Eudora.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
                  .ConfigureAppConfiguration(ConfigConfiguration)
                  .UseStartup<Startup>()
                  .Build();

        static void ConfigConfiguration(WebHostBuilderContext context, IConfigurationBuilder config)
        {
            config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);

        }
    }
}
