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
using System.Text;
using Microsoft.Extensions.Logging;

namespace FLBA
{
    /// <summary>
    /// Provides methods to log messages to file
    /// </summary>
    public class FileLogger : ILogger, IDisposable
    {
        private string _categoryName;
        
        private LogLevel _logLevel;

        private StreamWriter _fileStream;

        private void StreamFlushMethod()
        {
            if ((++_logCount) >= 5)
            {
                _fileStream.Flush();
                _logCount = 0;
            }
        }
        
        private int _logCount;
        
        /// <summary>
        /// Initialized new FileLogger object with specified file path
        /// </summary>
        /// <param name="filePath">Path to log file (it will be overwritten)</param>
        /// <param name="categoryName">Category name of the current logger (will be added to the log messages)</param>
        /// <param name="autoWrite">Will the buffer be written to the file after each Log()</param>
        /// <param name="logLevel">Current log level of the logger</param>
        /// <exception cref="ArgumentException">raised if logLevel is LogLevel.None</exception>
        public FileLogger(string filePath, string categoryName, bool autoWrite = true, LogLevel logLevel = LogLevel.Warning)
        {
            if (logLevel == LogLevel.None) throw new ArgumentException("Log level shouldn't be None", nameof(logLevel));

            _categoryName = categoryName;
            _logCount = 0;
            _logLevel = logLevel;
            
            File.CreateText(filePath).Dispose();
            _fileStream = new StreamWriter(filePath, false, Encoding.Unicode, 512);
            _fileStream.AutoFlush = autoWrite;
        }

        /// <summary>
        /// Get or set whether the buffer will be written to the file after each Log()
        /// </summary>
        public bool AutoWrite
        {
            get => _fileStream.AutoFlush;
            set => _fileStream.AutoFlush = value;
        }

        ~FileLogger()
        {
            Dispose();
        }

        /// <summary>
        /// Log message to the file
        /// </summary>
        /// <param name="logLevel">Log level of the message</param>
        /// <param name="eventId">Id of the event</param>
        /// <param name="state">State to log to the file</param>
        /// <param name="exception">Throwed exception</param>
        /// <param name="formatter">Function to format state and exception</param>
        /// <typeparam name="TState">Type of state (need .ToString() overrided method)</typeparam>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (logLevel == LogLevel.None) return;
            string formattedString = $"{state.ToString()}\n{exception}";
                
            string line =
                $"[{DateTime.Now.ToString()}] {_categoryName}\n\t{Enum.GetName(typeof(LogLevel), logLevel)}:\n{formattedString}\n";
            lock (_fileStream)
            {
                _fileStream.WriteLine(line);
                if(!_fileStream.AutoFlush) StreamFlushMethod();
            }
        }

        /// <summary>
        /// Check if log level is enabled
        /// </summary>
        /// <param name="logLevel">Log level to check</param>
        /// <returns>true, if <paramref name="logLevel"/> enabled, otherwise false</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None && logLevel >= _logLevel;
        }

        /// <summary>
        /// Useless.
        /// Returns current object.
        /// </summary>
        /// <param name="state">...</param>
        /// <typeparam name="TState">...</typeparam>
        /// <returns>Current FileLogger object</returns>
        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return this;
        }

        /// <summary>
        /// Flush log buffer and release resources
        /// </summary>
        public void Dispose()
        {
            _fileStream.Flush();
            _fileStream.Dispose();
        }
    }
}