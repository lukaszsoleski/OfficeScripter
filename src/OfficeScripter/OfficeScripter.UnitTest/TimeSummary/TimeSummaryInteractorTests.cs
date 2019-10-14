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
using Xunit;

namespace OfficeScripter.UnitTest.TimeSummary
{
    public class TimeSummaryInteractorTests
    {
        // TODO:
        // Add more events 
        // Create events in diffrent time blocks then required

        [Fact]
        public void GetTimeSummary_SumWorkTimeInTheGivenTimeBlock()
        {
            var mocker = new AutoMocker();
            var startDate = new DateTime(2019, 10, 1, 8,0,0);
            var hoursToAdd = 8;
            var events = new List<TimeSummaryEvent>()
            {
                new TimeSummaryEvent()
                {
                    CreatedAt = startDate,
                    EventType = EventTypeEnum.WorkStart,
                    ProjectType = ProjectTypeEnum.Unknown
                },
                new TimeSummaryEvent()
                {
                    CreatedAt = startDate.AddHours(hoursToAdd),
                    EventType = EventTypeEnum.WorkEnd,
                    ProjectType = ProjectTypeEnum.Unknown
                }
            };
            var timeBlock = new TimeBlock();
            var interactor = mocker.CreateInstance<TimeSummaryInteractor>();
            var timePeriod = TimePeriodTypeEnum.Month;

            // act
            var summary = interactor.GetTimeSummary(events, timeBlock, timePeriod);
            // assert
            summary.TimeSpan.TotalHours.Should().Be(8);
        }

    }
}
