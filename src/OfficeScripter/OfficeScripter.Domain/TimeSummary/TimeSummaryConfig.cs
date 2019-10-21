using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OfficeScripter.Domain.TimeSummary
{
    public class TimeSummaryConfig
    {
      
        public TimeSpan EnLessonTime { get; set; }

        public string WorkStartName { get; set; }
        public string WorkEndName { get; set; }
        public string WorkBreakStartName { get; set; }
        public string WorkBreakEndName { get; set; }
        public string EnLessonName { get; set; }

        public string DateTimeFormat { get; set; }

        public string ProjectHeaderName { get; set; }
        public string EventHeaderName { get; set; }
        public string CreatedAtHeaderName { get; set; }

        public EventTypeEnum ParseEventType(string eventName)
        {
            EventTypeEnum eventType = EventTypeEnum.Unknown;

            if (eventName == WorkStartName)
                eventType = EventTypeEnum.WorkStart;
            else if (eventName == WorkEndName)
                eventType = EventTypeEnum.WorkEnd;
            else if (eventName == WorkBreakStartName)
                eventType = EventTypeEnum.WorkBreakStart;
            else if (eventName == WorkBreakEndName)
                eventType = EventTypeEnum.WorkBreakEnd;

            return eventType;
        }

        public ProjectTypeEnum ParseProjectType(string projectName)
        {
            ProjectTypeEnum project = ProjectTypeEnum.Unknown;
            if (projectName == EnLessonName) 
                project = ProjectTypeEnum.EnglishLesson;
            return project;
        }

        public DateTime ConvertToDate(string dateStr)
        {
            return DateTime.ParseExact(dateStr, DateTimeFormat, CultureInfo.InvariantCulture);
        }

    }
}
