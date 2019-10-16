using OfficeScripter.Domain;
using OfficeScripter.Domain.TimeSummary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace OfficeScripter.UnitTest.TimeSummary.Helpers
{
    public static class SampleData
    {
        public static DateTime EventsStartDate { get; set; } = new DateTime(2019, 10, 2, 8, 0, 0);
        public static List<TimeSummaryEvent> Events => new List<TimeSummaryEvent>()
            {
                // Work start + 0:00
                new TimeSummaryEvent()
        {
            CreatedAt = EventsStartDate,
                    EventType = EventTypeEnum.WorkStart,
                    ProjectType = ProjectTypeEnum.Unknown
                },
                // Work break start +4:00  
                new TimeSummaryEvent()
        {
            CreatedAt = EventsStartDate.AddHours(4),
                    EventType = EventTypeEnum.WorkBreakStart,
                    ProjectType = ProjectTypeEnum.Unknown
                },
                // Work break end +6:30  
                new TimeSummaryEvent()
        {
            CreatedAt = EventsStartDate.AddHours(6).AddMinutes(30),
                    EventType = EventTypeEnum.WorkBreakEnd,
                    ProjectType = ProjectTypeEnum.Unknown
                },
                // Work end +8:00           -- 5:30 of the work time
                new TimeSummaryEvent()
        {
            CreatedAt = EventsStartDate.AddHours(8),
                    EventType = EventTypeEnum.WorkEnd,
                    ProjectType = ProjectTypeEnum.Unknown
                },
                // Next day work start +24:00
                 new TimeSummaryEvent()
        {
            CreatedAt = EventsStartDate.AddDays(1),
                    EventType = EventTypeEnum.WorkStart,
                    ProjectType = ProjectTypeEnum.Unknown
                },
                 // English lesson +25:00
                   new TimeSummaryEvent()
        {
            CreatedAt = EventsStartDate.AddDays(1).AddHours(1),
                    EventType = EventTypeEnum.Unknown,
                    ProjectType = ProjectTypeEnum.EnglishLesson
                },
                 // Work end +31:00         -- 7h - enLesson(1.5h default)
                new TimeSummaryEvent()
        {
            CreatedAt = EventsStartDate.AddDays(1).AddHours(7).AddMinutes(15),
                    EventType = EventTypeEnum.WorkEnd,
                    ProjectType = ProjectTypeEnum.Unknown
                },
                
                // sum ~= 11h15min               
            };
    }
}
