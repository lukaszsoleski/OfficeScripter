using ExcelMapper;
using Microsoft.Extensions.Logging;
using OfficeScripter.Abstractions.TimeSummary;
using OfficeScripter.Domain.TimeSummary;
using OfficeScripter.Infrastructure.ClassMapReader.Configuration;
using OfficeScripter.Infrastructure.ClassMapReader.Factory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OfficeScripter.Infrastructure.ReadExcel
{
    public class ClassMapExcelReader : IClassMapExcelReader
    {
        private readonly ClassMapFactory factory;
        private readonly ILogger<ClassMapExcelReader> logger;

        public ClassMapExcelReader(ClassMapFactory factory, ILogger<ClassMapExcelReader> logger)
        {
            this.factory = factory;
            this.logger = logger;
        }
       

        public IEnumerable<T> Load<T>(Stream stream)
        {
            var mapper = ResolveMapper<T>();

            if (mapper == null) return default;

            using var importer = new ExcelImporter(stream);

            importer.Configuration.RegisterClassMap(mapper);

            // TODO: Add configuration for sheet name or index
            var sheet = importer.ReadSheet();

            var data = sheet.ReadRows<T>().ToList();

            return data;
        }

        public IEnumerable<T> Load<T>(string path)
        {
            using var stream = File.OpenRead(path);

            return Load<T>(stream);
        }

        private ExcelClassMap ResolveMapper<T>()
        {
            var mapper = factory.CreateMapper<T>();

            if (mapper == null)
            {
                logger.LogError($"No ClassMap configuration for the {typeof(T)} type.");

                return default;
            }
            return mapper;
        }
    }
}
