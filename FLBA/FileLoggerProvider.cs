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

using System.IO;
using Microsoft.Extensions.Logging;

namespace FLBA
{
    /// <summary>
    /// Provide method to create FileLogger object
    /// </summary>
    public class FileLoggerProvider : ILoggerProvider
    {
        private string _filePath;
        
        /// <summary>
        /// Initialized new FileLoggerProvider object with specified file path
        /// </summary>
        /// <param name="filePath"></param>
        public FileLoggerProvider(string filePath)
        {
            _filePath = filePath;
        }
        
        /// <summary>
        /// Dont do anything
        /// </summary>
        public void Dispose()
        { }

        /// <summary>
        /// Create new FileLogger instance with specified category name
        /// </summary>
        /// <param name="categoryName">category name</param>
        /// <returns>FileLogger instance</returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_filePath, categoryName);
        }
    }
}