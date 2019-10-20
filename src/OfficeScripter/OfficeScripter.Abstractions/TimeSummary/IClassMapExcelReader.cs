using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OfficeScripter.Abstractions.TimeSummary
{
    public interface IClassMapExcelReader
    {
        public IEnumerable<T> Load<T>(Stream stream);
        public IEnumerable<T> Load<T>(string path);
    }
}
