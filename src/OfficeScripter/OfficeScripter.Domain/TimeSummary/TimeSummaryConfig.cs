using System;
using System.Collections.Generic;
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
    }
}
