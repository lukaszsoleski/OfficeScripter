using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeScripter.Domain.TimeSummary
{
    /// <summary>
    /// An event registered in the system.
    /// </summary>
    public class TimeSummaryEvent
    {
        /// <summary>
        /// Registered event type.
        /// </summary>
        public EventTypeEnum EventType { get; set; }
        /// <summary>
        /// The project in which the event was registered.
        /// </summary>
        public ProjectTypeEnum ProjectType { get; set; }
        /// <summary>
        /// The time when the event was registered.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Checks if the event is a known type.
        /// </summary>
        public bool HasEvent => !(EventType == EventTypeEnum.Unknown && ProjectType == ProjectTypeEnum.Unknown);
        public override string ToString()
        {
            return $"Event {EventType} has been registered at {CreatedAt} in the [{ProjectType}] project.";
        }
    }
}
