using System;
using ExcelMapper;
namespace OfficeScripter.Infrastructure
{
    public interface IClassMapFactory
    {
        ExcelClassMap CreateMapper<T>();
    }
}
