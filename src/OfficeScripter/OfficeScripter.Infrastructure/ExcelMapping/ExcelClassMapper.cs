using Ganss.Excel;
using OfficeScripter.Abstractions.TimeSummary;
using OfficeScripter.Infrastructure.ExcelMapping.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;

namespace OfficeScripter.Infrastructure.ExcelMapping
{
    public class ExcelClassMapper : IExcelClassMap
    {
        private readonly IFileSystem _fs;
        private readonly ExcelMapperConfiguration _mapperConfiguration;

        public ExcelClassMapper(IFileSystem fs, ExcelMapperConfiguration mapperConfiguration)
        {
            _fs = fs;
            _mapperConfiguration = mapperConfiguration;
        }
        public IEnumerable<T> ReadRows<T>(Stream stream) where T: new()
        {
            var mapper = new ExcelMapper(stream);

            _mapperConfiguration.ProvideConfiguration(mapper);

            return mapper.Fetch<T>();
        }

        public IEnumerable<T> ReadRows<T>(string path) where T: new()
        {
            using var fileStream = File.OpenRead(path);

            return ReadRows<T>(fileStream);
            
        }
        
    }
}
