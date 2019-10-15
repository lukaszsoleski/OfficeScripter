using Bogus;
using FluentAssertions;
using Itenso.TimePeriod;
using Moq.AutoMock;
using OfficeScripter.Abstractions.TimeSummary;
using OfficeScripter.Application.TimeSummary;
using OfficeScripter.Domain;
using OfficeScripter.Domain.TimeSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using Xunit;

namespace OfficeScripter.UnitTest.TimeSummary
{
    public class TimeSummaryInteractorTests
    {

        [Fact]
        public void GetTimeSummary_SumWorkTimeInTheGivenTimeBlock()
        {
            #region Arrange
            // Create automocker container
            var mocker = new AutoMocker();
            // Set start point
            var startDate = new DateTime(2019, 10, 2, 8, 0, 0);
            // Set time block for the calculation
            var timeBlock = new TimeBlock(startDate, startDate.AddDays(2));
            // Generate sample events. It is important to keep this method constant.
            var events = SampleTestData(startDate);
            #endregion
            #region Act
            var interactor = mocker.CreateInstance<TimeSummaryInteractor>();
            var summary = interactor.GetTimeSummary(events, timeBlock);
            #endregion
            #region Assert
            summary.TimeSpan.TotalHours.Should().Be(8);
            #endregion
        }

        [Fact]
        public void GetTimeSummary_IgnoreEventsOutsideOfAGivenTimeBlock()
        {

        }
        [Theory]
        [InlineData(EventTypeEnum.WorkStart)]
        [InlineData(EventTypeEnum.WorkEnd)]
        [InlineData(EventTypeEnum.WorkBreakEnd)]
        [InlineData(EventTypeEnum.WorkBreakStart)]
        void GetTimeSummary_EventIsMissing_LogCritical(EventTypeEnum eventType)
        {

        }
        /// <summary>
        /// Sample data for testing purposes.
        /// </summary>
        private List<TimeSummaryEvent> SampleTestData(DateTime startDate)
        {


            var events = new List<TimeSummaryEvent>()
            {
                // Work start + 0:00
                new TimeSummaryEvent()
                {
                    CreatedAt = startDate,
                    EventType = EventTypeEnum.WorkStart,
                    ProjectType = ProjectTypeEnum.Unknown
                },
                // Work break start +4:00  
                new TimeSummaryEvent()
                {
                    CreatedAt = startDate.AddHours(4), 
                    EventType = EventTypeEnum.WorkBreakStart,
                    ProjectType = ProjectTypeEnum.Unknown
                },
                // Work break end +6:30  
                new TimeSummaryEvent()
                {
                    CreatedAt = startDate.AddHours(6).AddMinutes(30),
                    EventType = EventTypeEnum.WorkBreakEnd,
                    ProjectType = ProjectTypeEnum.Unknown
                },
                // Work end +8:00           -- 5:30 of the work time
                new TimeSummaryEvent()
                {
                    CreatedAt = startDate.AddHours(8), 
                    EventType = EventTypeEnum.WorkEnd,
                    ProjectType = ProjectTypeEnum.Unknown
                },
                // Next day work start +24:00
                 new TimeSummaryEvent()
                {
                    CreatedAt = startDate.AddDays(1), 
                    EventType = EventTypeEnum.WorkStart,
                    ProjectType = ProjectTypeEnum.Unknown
                },
                 // English lesson +25:00
                   new TimeSummaryEvent()
                {
                    CreatedAt = startDate.AddDays(1).AddHours(1),
                    EventType = EventTypeEnum.Unknown,
                    ProjectType  = ProjectTypeEnum.EnglishLesson
                },
                 // Work end +31:00         -- 7h - enLesson(1.5h default)
                new TimeSummaryEvent()
                {
                    CreatedAt = startDate.AddDays(1).AddHours(7).AddMinutes(15),
                    EventType = EventTypeEnum.WorkEnd,
                    ProjectType = ProjectTypeEnum.Unknown
                },
                
                // sum ~= 11h15min               
            };
            return events;

        }

    }
}
