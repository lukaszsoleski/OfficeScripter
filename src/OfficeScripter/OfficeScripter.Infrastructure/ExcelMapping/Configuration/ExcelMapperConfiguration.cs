using Ganss.Excel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OfficeScripter.Domain.TimeSummary;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeScripter.Infrastructure.ExcelMapping.Configuration
{
    public class ExcelMapperConfiguration
    {
        private readonly TimeSummaryConfig tsc;

        public ExcelMapperConfiguration(IOptionsMonitor<TimeSummaryConfig> options)
        {
            tsc = options.CurrentValue;
        }

        public ExcelMapper ProvideConfiguration(ExcelMapper mapper)
        {
            MapTimeSummaryEvent(mapper);
            return mapper;
        }

        private void MapTimeSummaryEvent(ExcelMapper mapper)
        {
            
            mapper.AddMapping<TimeSummaryEvent>(tsc.EventHeaderName, x => x.EventType)
                .SetPropertyUsing(v => tsc.ParseEventType(v as string));
            mapper.AddMapping<TimeSummaryEvent>(tsc.ProjectHeaderName, x => x.ProjectType)
                .SetPropertyUsing(v => tsc.ParseProjectType(v as string));
            mapper.AddMapping<TimeSummaryEvent>(tsc.CreatedAtHeaderName, x => x.CreatedAt)
                .SetPropertyUsing(x => tsc.ConvertToDate(x));
        }
    }
}
