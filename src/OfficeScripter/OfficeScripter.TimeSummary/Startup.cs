using System;
using System.IO;
using System.Linq;
using Itenso.TimePeriod;
using Microsoft.Extensions.Logging;
using OfficeScripter.Abstractions.TimeSummary;
using OfficeScripter.Domain.TimeSummary;

namespace OfficeScripter.TimeSummary
{
    public class Startup
    {
        private readonly ILogger<Startup> logger;
        private readonly IExcelClassMap _mapper;
        private readonly ITimeSummary _timeSummary;

        public Startup(ILogger<Startup> logger, IExcelClassMap mapper, ITimeSummary timeSummary)
        {
            this.logger = logger;
            _mapper = mapper;
            _timeSummary = timeSummary;
        }

        public void Run()
        {
            RunTimeSummary();

        }

        private void RunTimeSummary()
        {
            var defaultPath = GetDefaultPath();

            InformAboutDefaultPath(defaultPath);

            var data = _mapper.ReadRows<TimeSummaryEvent>(defaultPath).ToList();
            var timeRange = new TimeRange(new DateTime(2019, 9, 1), new DateTime(2019, 9, 20));
            var summary = _timeSummary.GetTimeSummary(data, timeRange);
            var dailySummary = _timeSummary.GetDailySummary(data, timeRange)
                .OrderBy(x => x.Date).ToList();
            var resultPath = defaultPath
                .Replace(Path.GetFileNameWithoutExtension(defaultPath), "Podsumowanie");
            _mapper.WriteRows(resultPath, dailySummary);
        }

        private void InformAboutDefaultPath(string defaultPath)
        {
            Console.WriteLine($"Place your file here: {defaultPath}");
            Console.WriteLine("Done? Press any key to continue.");
            Console.ReadKey();
  
        }

        private string GetDefaultPath()
        {
            var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            return Path.Combine(documentsDirectory, "OfficeScripter/TimeSummary/Raport.xlsx");
        }
    }
}
