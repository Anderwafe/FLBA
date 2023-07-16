using System;
using Microsoft.Extensions.Logging;

namespace FLBA
{
    public class FileLogger<T> : FileLogger
    {
        public FileLogger(string filePath, LogLevel logLevel = LogLevel.Warning) 
            : base(filePath, typeof(T).Name, logLevel)
        {
            
        }
    }
}