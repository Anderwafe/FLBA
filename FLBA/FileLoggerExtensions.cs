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
using Microsoft.Extensions.Logging;

namespace FLBA.Extensions
{
    /// <summary>
    /// Provides static method for the add FileLoggerProvider to specified LoggingBuilder
    /// </summary>
    public static class FileLoggerExtensions
    {
        /// <summary>
        /// Add FileLoggerProvider with specified filePath to the builder
        /// </summary>
        /// <param name="builder">Logging builder object to attach FileLoggerProvider to</param>
        /// <param name="filePath">File path for FileLoggerProvider</param>
        /// <returns>builder with FileLoggerProvider</returns>
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filePath = null)
        {
            filePath ??= Path.Join(Environment.CurrentDirectory, ".log");
            builder.AddProvider(new FileLoggerProvider(filePath));
            return builder;
        }
    }
}