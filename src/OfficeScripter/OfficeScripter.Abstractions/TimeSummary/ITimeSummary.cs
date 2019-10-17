using OfficeScripter.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OfficeScripter.Abstractions.TimeSummary
{
    using Itenso.TimePeriod;
    using OfficeScripter.Domain.TimeSummary;
    public interface ITimeSummary
    {
      int Goal(ITimeBlock timePeriod);

      TimeSpan GetTimeSummary(IEnumerable<TimeSummaryEvent> events, ITimePeriod timePeriod);
      
    }
}
