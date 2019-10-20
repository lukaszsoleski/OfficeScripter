using ExcelMapper;
using OfficeScripter.Domain;
using OfficeScripter.Domain.TimeSummary;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace OfficeScripter.Infrastructure.ClassMapReader.Configuration
{
    public class TimeSummaryEventClassMap : ExcelClassMap<TimeSummaryEvent>
    {
        public TimeSummaryConfig Config { get; set; }
        public TimeSummaryEventClassMap()
        {
        }

        public void ConfigureMapping()
        {
            EventsMap();
            CreationTImeMap();
            ProjectTypeMap();
        }

        private void ProjectTypeMap()
        {
            Map(x => x.ProjectType).WithColumnName(Config.ProjectHeaderName)
                .WithEmptyFallback(ProjectTypeEnum.Unknown)
                .WithInvalidFallback(ProjectTypeEnum.Unknown)
                .WithMapping(new Dictionary<string, ProjectTypeEnum>()
                {
                    {Config.EnLessonName, ProjectTypeEnum.EnglishLesson }
                });
        }

        private void CreationTImeMap()
        {
            Map(x => x.CreatedAt).WithColumnName(Config.CreatedAtHeaderName)
                .WithConverter(ToDateTime);
        }

        private void EventsMap()
        {
            Map(x => x.EventType).WithColumnName(Config.EventHeaderName)
                .WithEmptyFallback(EventTypeEnum.Unknown)
                .WithInvalidFallback(EventTypeEnum.Unknown)
                .WithMapping(new Dictionary<string, EventTypeEnum>()
                {
                    {Config.WorkStartName, EventTypeEnum.WorkStart },
                    {Config.WorkEndName, EventTypeEnum.WorkEnd },
                    {Config.WorkBreakStartName, EventTypeEnum.WorkBreakStart },
                    {Config.WorkBreakEndName, EventTypeEnum.WorkBreakEnd },
                });
        }

        private DateTime ToDateTime(string date)
        {
            return DateTime.ParseExact(date, Config.DateTimeFormat, CultureInfo.InvariantCulture);
        }
    }
}
