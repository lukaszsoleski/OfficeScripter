using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OfficeScripter.Domain.TimeSummary;
using OfficeScripter.Infrastructure.ExcelMapping.Extensions;
using System;
using System.IO;
using System.IO.Abstractions;
using System.Text;

namespace OfficeScripter.TimeSummary
{

    public class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            // Create service collection and configure our services
            var services = ConfigureServices();
            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();

            // Kick off our actual code
            serviceProvider.GetService<Startup>().Run();
        }
        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true,
                             reloadOnChange: true);
            return builder.Build();
        }
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            // IMPORTANT! Register our application entry point
            services.AddTransient<Startup>();

            var config = LoadConfiguration();

            services.Configure<TimeSummaryConfig>(config.GetSection("TimeSummary"));
            
            AddLogging(services, config);

            AddApplicationServices(services);

            AddFrameworkServices(services);
            return services;
        }

        private static void AddFrameworkServices(IServiceCollection services)
        {
            services.AddTransient<IFileSystem, FileSystem>();
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddExcelMapper();
        }

        private static void AddLogging(IServiceCollection services, IConfiguration config)
        {
            services.AddLogging(logging =>
            {
                logging.AddConfiguration(config.GetSection("Logging"));
                logging.AddConsole();
            });
        }
    }
}
