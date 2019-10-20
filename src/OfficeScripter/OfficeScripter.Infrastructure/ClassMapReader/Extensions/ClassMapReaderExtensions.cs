using Microsoft.Extensions.DependencyInjection;
using OfficeScripter.Abstractions.TimeSummary;
using OfficeScripter.Infrastructure.ClassMapReader.Configuration;
using OfficeScripter.Infrastructure.ClassMapReader.Factory;
using OfficeScripter.Infrastructure.ReadExcel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeScripter.Infrastructure.ClassMapReader.Extensions
{
    public static class ClassMapReaderExtensions
    {
        public static IServiceCollection AddExcelClassMapReader(this IServiceCollection services)
        {
            // Register helper ClassMap factory
            services.AddTransient<IClassMapFactory, ClassMapFactory>();

            // Register excel reader class
            services.AddTransient<IClassMapExcelReader, ClassMapExcelReader>();

            // Return collection for chaining
            return services;
        }
    }
}
