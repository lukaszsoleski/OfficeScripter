using Itenso.TimePeriod;
using OfficeScripter.Abstractions.TimeSummary;
using OfficeScripter.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OfficeScripter.Application.TimeSummary
{
    using OfficeScripter.Domain.TimeSummary;
    public class TimeSummaryInteractor : ITimeSummary
    {
        /// <summary>
        /// Time summary for the given time block grouped by the day, month eg.
        /// </summary>
        /// <param name="events">List of imported events.</param>
        /// <param name="timeBlock">The time block according to which the calculations are to be performed.</param>
        /// <param name="timePeriod">The type of time period of the summary</param>
        /// <returns></returns>
        public IEnumerable<TimeSummary> GetTimeSummary(IEnumerable<TimeSummaryEvent> events, ITimeBlock timeBlock, TimePeriodEnum timePeriod)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get work hours for the given time block.
        /// </summary>
        /// <param name="timePeriod">The time block according to which the calculations are to be performed.</param>
        /// <returns></returns>
        public int Goal(ITimeBlock timePeriod)
        {
            throw new NotImplementedException();
        }
    }
}
