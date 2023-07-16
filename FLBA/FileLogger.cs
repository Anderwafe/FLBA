using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using Microsoft.Extensions.Logging;

namespace FLBA
{
    public class FileLogger : ILogger, IDisposable
    {
        private string _categoryName;
        
        private LogLevel _logLevel;

        private StreamWriter _fileStream;

        private int _logCount;
        
        public FileLogger(string filePath, string categoryName, LogLevel logLevel = LogLevel.Warning)
        {
            if (logLevel == LogLevel.None) throw new ArgumentException("Log level shouldn't be None", nameof(logLevel));

            _categoryName = categoryName;
            _logCount = 0;
            _logLevel = logLevel;
            _fileStream = File.CreateText(filePath);
            _fileStream = new StreamWriter(filePath, false, Encoding.Unicode, 512);
            _fileStream.AutoFlush = false;
        }

        ~FileLogger()
        {
            Dispose();
        }
        
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (logLevel == LogLevel.None) return;
            string formattedString = $"{state.ToString()}\n{exception}";
                
            string line =
                $"[{DateTime.Now.ToString()}] {_categoryName}\n\t{Enum.GetName(typeof(LogLevel), logLevel)}:\n{formattedString}\n";
            lock (_fileStream)
            {
                _fileStream.WriteLine(line);
                if ((++_logCount) >= 5)
                {
                    _fileStream.Flush();
                    _logCount = 0;
                }
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None && logLevel >= _logLevel;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return this;
        }

        public void Dispose()
        {
            _fileStream.Flush();
            _fileStream.Dispose();
        }
    }
}