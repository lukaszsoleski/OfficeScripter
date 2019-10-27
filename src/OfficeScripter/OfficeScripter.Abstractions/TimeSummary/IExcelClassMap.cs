using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OfficeScripter.Abstractions.TimeSummary
{
    public interface IExcelClassMap
    {
        public IEnumerable<T> ReadRows<T>(Stream stream) where T : new();
        public IEnumerable<T> ReadRows<T>(string path) where T: new();
        void WriteRows<T>(string path, IEnumerable<T> rows, string sheetName = "summary");
    }
}
