using Itenso.TimePeriod;
using OfficeScripter.Abstractions.TimeSummary;
using OfficeScripter.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OfficeScripter.Application.TimeSummary
{
    using Microsoft.Extensions.Logging;
    using OfficeScripter.Domain.TimeSummary;
    public class TimeSummaryInteractor : ITimeSummary
    {
        private readonly ILogger<TimeSummaryInteractor> logger;

        public TimeSummaryInteractor(ILogger<TimeSummaryInteractor> logger)
        {
            this.logger = logger;
        }
        /// <summary>
        /// Time summary for the given time block grouped by the day, month eg. 
        /// </summary>
        /// <param name="events">List of imported events.</param>
        /// <param name="timeBlock">The time block according to which the calculations are to be performed.</param>
        /// <param name="timePeriod">The type of time period of the summary</param>
        /// <returns></returns>
        public TimeSummary GetTimeSummary(IEnumerable<TimeSummaryEvent> events, ITimeBlock timeBlock)
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
            // Holidays API
            // 1000 Non commercial use only Attribution Required
            // https://calendarific.com/account
            throw new NotImplementedException();
        }
    }
}
