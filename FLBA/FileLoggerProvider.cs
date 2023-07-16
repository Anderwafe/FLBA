using System.IO;
using Microsoft.Extensions.Logging;

namespace FLBA
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string _filePath;
        
        public FileLoggerProvider(string filePath)
        {
            _filePath = filePath;
        }
        
        public void Dispose()
        { }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_filePath, categoryName);
        }
    }
}