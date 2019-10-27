using System;

namespace OfficeScripter.Domain.TimeSummary
{
    public class DayTimeSummary
    {
        public DayTimeSummary(DateTime date, TimeSpan time)
        {
            Date = date;
            Time = time;
        }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Hours => Time.Hours;
        public int Minutes => Time.Minutes;
        public string Formatted => $"{Time.Hours}h{Time.Minutes}min";
        
    }
}
