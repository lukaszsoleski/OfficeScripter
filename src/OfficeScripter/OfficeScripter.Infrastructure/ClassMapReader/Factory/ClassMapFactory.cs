using ExcelMapper;
using OfficeScripter.Domain.TimeSummary;
using OfficeScripter.Infrastructure.ClassMapReader.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeScripter.Infrastructure.ClassMapReader.Factory
{
    /// <summary>
    /// Create ExcelClassMap objects that require additional configuration.
    /// </summary>
    public class ClassMapFactory : IClassMapFactory
    {
        private readonly TimeSummaryConfig timeSummaryConfig;

        public ClassMapFactory(TimeSummaryConfig timeSummaryConfig)
        {
            this.timeSummaryConfig = timeSummaryConfig;
        }
        public ExcelClassMap CreateMapper<T>()
        {
            if(typeof(T) == typeof(TimeSummaryEventClassMap))
            {
                return CreateTimeSummaryClassMap();
            }
            return null;
        }
        private TimeSummaryEventClassMap CreateTimeSummaryClassMap()
        {
            return new TimeSummaryEventClassMap() { Config = timeSummaryConfig };
        }
    }
}
