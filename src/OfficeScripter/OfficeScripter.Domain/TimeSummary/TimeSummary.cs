using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeScripter.Domain.TimeSummary
{
    public class TimeSummary
    {
        public TimePeriodEnum TimePeriod { get; set; }

        public TimeSpan TimeSpan { get; set; }
    }
}
