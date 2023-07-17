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
using Microsoft.Extensions.Logging;

namespace FLBA
{
    /// <summary>
    /// Provides methods to log messages to file
    /// </summary>
    /// <typeparam name="T">Category name</typeparam>
    public class FileLogger<T> : FileLogger
    {
        /// <summary>
        /// Initialized new FileLogger object with specified file path
        /// </summary>
        /// <param name="filePath">Path to log file (it will be overwritten)</param>
        /// <param name="autoWrite">Will the buffer be written to the file after each Log()</param>
        /// <param name="logLevel">Current log level of the logger</param>
        public FileLogger(string filePath, bool autoWrite = true, LogLevel logLevel = LogLevel.Warning) 
            : base(filePath, typeof(T).Name, autoWrite, logLevel)
        {
            
        }
    }
}