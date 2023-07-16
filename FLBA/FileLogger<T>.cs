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
    public class FileLogger<T> : FileLogger
    {
        public FileLogger(string filePath, LogLevel logLevel = LogLevel.Warning) 
            : base(filePath, typeof(T).Name, logLevel)
        {
            
        }
    }
}