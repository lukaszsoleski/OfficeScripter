using System;
using System.Collections.Generic;

namespace OfficeScripter.Abstractions.TimeSummary
{
    using Itenso.TimePeriod;
    using OfficeScripter.Domain.TimeSummary;
    public interface ITimeSummary
    {
      int Goal(ITimeBlock timePeriod);

      TimeSpan GetTimeSummary(IEnumerable<TimeSummaryEvent> events, ITimePeriod timePeriod);
      IEnumerable<DayTimeSummary> GetDailySummary(IEnumerable<TimeSummaryEvent> events, ITimePeriod timePeriod);
    }
}
