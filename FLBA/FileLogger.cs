/*
    Copyright © 2023 Anderwafe. All rights reserved.
 
    Copyright 2023 Anderwafe

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, 
    software distributed under the License is distributed on an "AS IS" BASIS, 
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and limitations under the License.
*/

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