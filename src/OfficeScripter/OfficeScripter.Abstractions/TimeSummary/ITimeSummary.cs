﻿using OfficeScripter.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OfficeScripter.Abstractions.TimeSummary
{
    using Itenso.TimePeriod;
    using OfficeScripter.Domain.TimeSummary;
    public interface ITimeSummary
    {
      int Goal(ITimeBlock timePeriod);

      TimeSummary GetTimeSummary(IEnumerable<TimeSummaryEvent> events, ITimeBlock timeBlock);
      
    }
}
