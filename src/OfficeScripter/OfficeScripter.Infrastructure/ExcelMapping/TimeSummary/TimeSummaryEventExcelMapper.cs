using Ganss.Excel;
using OfficeScripter.Domain.TimeSummary;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeScripter.Infrastructure.ExcelMapping.TimeSummary
{
    public class TimeSummaryEventExcelMapper
    {
        private readonly TimeSummaryConfig tsc;

        public TimeSummaryEventExcelMapper(TimeSummaryConfig tsc)
        {
            this.tsc = tsc;
        }

        public ExcelMapper AddMapping(ExcelMapper mapper)
        {
            mapper.AddMapping<TimeSummaryEvent>(tsc.EventHeaderName, x => x.EventType)
                .SetPropertyUsing(v => tsc.ParseEventType(v as string));
            mapper.AddMapping<TimeSummaryEvent>(tsc.ProjectHeaderName, x => x.ProjectType)
                .SetPropertyUsing(v => tsc.ParseProjectType(v as string));
            mapper.AddMapping<TimeSummaryEvent>(tsc.CreatedAtHeaderName, x => x.CreatedAt)
                .SetPropertyUsing(x => tsc.ConvertToDate(x as string));

            return mapper;
        }
    }
}
