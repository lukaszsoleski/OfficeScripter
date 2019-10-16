using Bogus;
using Castle.Core.Logging;
using FluentAssertions;
using Itenso.TimePeriod;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using OfficeScripter.Abstractions.TimeSummary;
using OfficeScripter.Application.TimeSummary;
using OfficeScripter.Domain;
using OfficeScripter.Domain.TimeSummary;
using OfficeScripter.UnitTest.TimeSummary.Helpers;
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
            var startDate = SampleData.EventsStartDate;
            // Set time block for the calculation
            var timeBlock = new TimeBlock(startDate, startDate.AddDays(2));
            // Generate sample events. It is important to keep this method constant.
            var events = SampleData.Events;
            #endregion
            #region Act
            var interactor = mocker.CreateInstance<TimeSummaryInteractor>();
            var summary = interactor.GetTimeSummary(events, timeBlock);
            #endregion
            #region Assert
            summary.TimeSpan.TotalMinutes.Should()
                .Be(TimeSpan.FromHours(11).Add(TimeSpan.FromMinutes(15)).TotalMinutes);

            #endregion
        }

        [Fact]
        public void GetTimeSummary_IgnoreEventsOutsideOfAGivenTimeBlock()
        {
            #region Arrange
            // Create automocker container
            var mocker = new AutoMocker();
            // Set start point
            var dateTime = SampleData.EventsStartDate;
            // Set time block for the calculation
            var singleDayTimeBlock = new TimeBlock(dateTime, dateTime);
            // Generate sample events. It is important to keep this method constant.
            var events = SampleData.Events;
            #endregion
            #region Act
            var interactor = mocker.CreateInstance<TimeSummaryInteractor>();
            var summary = interactor.GetTimeSummary(events, singleDayTimeBlock);
            #endregion
            #region Assert
            summary.TimeSpan.TotalMinutes.Should()
                .Be(new TimeSpan(5,30,0).TotalMinutes);

            #endregion


        }
        [Theory]
        [InlineData(EventTypeEnum.WorkStart)]
        [InlineData(EventTypeEnum.WorkEnd)]
        [InlineData(EventTypeEnum.WorkBreakEnd)]
        [InlineData(EventTypeEnum.WorkBreakStart)]
        void GetTimeSummary_EventIsMissing_LogCritical(EventTypeEnum eventType)
        {
            #region Arrange
            // Create automocker container
            var mocker = new AutoMocker();
            // Set start point
            var startDateTime = SampleData.EventsStartDate;
            // Set time block for the calculation
            var timeBlock = new TimeBlock(startDateTime, startDateTime.AddDays(2));
            // Generate sample events. It is important to keep this method constant.
            var events = SampleData.Events;
            // Find event to be modified
            var eventToBeModified = events.First(x => x.CreatedAt.Date == startDateTime.Date && x.EventType == eventType);
            // Change specified event to be unknown
            eventToBeModified.EventType = EventTypeEnum.Unknown;

            #endregion
            #region Act
            var interactor = mocker.CreateInstance<TimeSummaryInteractor>();
            var summary = interactor.GetTimeSummary(events, timeBlock);
            #endregion
            // verify that critical message was logged
            mocker.GetMock<ILogger<TimeSummaryInteractor>>().
                Verify(x => x.LogCritical(It.IsAny<string>()), Times.Once);

            //  check if the method adds up only one of the two days
            summary.TimeSpan.TotalHours.Should().BeLessThan(8);
        }

    }
}
