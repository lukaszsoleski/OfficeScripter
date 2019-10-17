using Itenso.TimePeriod;
using OfficeScripter.Abstractions.TimeSummary;
using OfficeScripter.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OfficeScripter.Application.TimeSummary
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using OfficeScripter.Domain.TimeSummary;
    using System.Linq;

    public class TimeSummaryInteractor : ITimeSummary
    {
        private readonly ILogger<TimeSummaryInteractor> logger;
        private readonly TimeSummaryConfig configuration;

        public TimeSummaryInteractor(ILogger<TimeSummaryInteractor> logger, TimeSummaryConfig configuration) 
        {
            this.logger = logger;
            this.configuration = configuration;
        }
        /// <summary>
        /// Time summary for the events grouped by the given time block.
        /// </summary>
        /// <param name="events">List of imported events.</param>
        /// <param name="timePeriod">The time block according to which the calculations are to be performed.</param>
        /// <returns></returns>
        public TimeSpan GetTimeSummary(IEnumerable<TimeSummaryEvent> events, ITimePeriod timePeriod)
        {
            var timeSummary = new TimeSpan(0);
            
            foreach (var dayEvents in GetDailyEvents(events, timePeriod))
            {
                var daySummary = SumDayEvents(dayEvents.Value);

                timeSummary.Add(daySummary);
            }
            return timeSummary;
        }
        /// <summary>
        /// Remove empty events and filter by time range.
        /// </summary>
        private Dictionary<DateTime,List<TimeSummaryEvent>> GetDailyEvents(IEnumerable<TimeSummaryEvent> events, ITimePeriod timePeriod)
        {
            // Remove unknown events and projects
            var filteredEvents = events.Where(x => x.HasEvent);
            // Filter by time range
            filteredEvents = FilterByTimeRange(timePeriod, filteredEvents);
            // Group events by day
            return filteredEvents.GroupBy(x => x.CreatedAt.Date).ToDictionary(x => x.Key, x=> x.ToList());
        }

        private IEnumerable<TimeSummaryEvent> FilterByTimeRange(ITimePeriod timePeriod, IEnumerable<TimeSummaryEvent> filteredEvents)
        {
            if (timePeriod.HasStart && timePeriod.HasEnd)
            {
                var timeRange = new TimeRange(timePeriod.Start, timePeriod.End);
               
                return  filteredEvents.Where(x => timeRange.HasInside(x.CreatedAt));
            }

            return filteredEvents;
        }

        private TimeSpan SumDayEvents(List<TimeSummaryEvent> dayEvents)
        {
            var workTime = SumActivityTime(dayEvents, EventTypeEnum.WorkStart, EventTypeEnum.WorkEnd);

            var breakTime = SumActivityTime(dayEvents, EventTypeEnum.WorkBreakStart, EventTypeEnum.WorkBreakEnd);

            var englishLesson = dayEvents.FirstOrDefault(x => x.ProjectType == ProjectTypeEnum.EnglishLesson);

            if (workTime == null) return TimeSpan.Zero;

            if(breakTime != null || breakTime != TimeSpan.Zero)
            {
                workTime -= breakTime;
            }
            if(englishLesson != null)
            {
                workTime -= configuration.EnLessonTime;
            }

            return workTime;
        }
     
        private TimeSpan SumActivityTime(List<TimeSummaryEvent> events, EventTypeEnum start, EventTypeEnum end)
        {
            var processStart = FindSingleEvent(start, events);

            var processEnd = FindSingleEvent(end, events);

            if (processStart == null || processEnd == null)
            {
                return TimeSpan.Zero;
            }

            return processEnd.CreatedAt - processStart.CreatedAt;
        }
        private TimeSummaryEvent FindSingleEvent(EventTypeEnum eventType, List<TimeSummaryEvent> events)
        {
           var tsEvents = events.Where(x => x.EventType == eventType).ToList();
           
            if (tsEvents.Count != 1)
            {
                logger.LogCritical($"Expected single event of type {eventType} but got {tsEvents.Count}.");
                return null;
            }
            return tsEvents.Single();
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
