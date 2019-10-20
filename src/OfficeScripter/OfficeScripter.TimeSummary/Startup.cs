using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OfficeScripter.TimeSummary
{
    public class Startup
    {
        private readonly ILogger<Startup> logger;

        public Startup(ILogger<Startup> logger)
        {
            this.logger = logger;
        }

        public void Run()
        {

            var defaultPath = GetDefaultPath();
            
            InformAboutDefaultPath(defaultPath);
            
            using var fileStream = File.OpenRead(defaultPath);



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
