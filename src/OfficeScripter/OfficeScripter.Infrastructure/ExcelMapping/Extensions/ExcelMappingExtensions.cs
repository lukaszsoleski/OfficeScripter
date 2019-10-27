using Ganss.Excel;
using Microsoft.Extensions.DependencyInjection;
using OfficeScripter.Abstractions.TimeSummary;
using OfficeScripter.Application.TimeSummary;
using OfficeScripter.Infrastructure.ExcelMapping.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OfficeScripter.Infrastructure.ExcelMapping.Extensions
{
   public static class ExcelMappingExtensions
    {
        public static IServiceCollection AddExcelMapper(this IServiceCollection services)
        {

            services.AddTransient<IExcelClassMap, ExcelClassMapper>();

            services.AddSingleton<ExcelMapperConfiguration>();

            services.AddTransient<ITimeSummary, TimeSummaryInteractor>();
            return services;
        }
    }
}
