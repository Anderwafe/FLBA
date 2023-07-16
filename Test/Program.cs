﻿/*
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
using System.Linq;
using FLBA;
using Microsoft.Extensions.Logging;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            FileLogger<Program> logger =
                new FileLogger<Program>(Path.Combine(Environment.CurrentDirectory, "test.log"));
            logger.Log<string>(LogLevel.Critical, 0, "Ok", null, null);
            logger.Log(LogLevel.Information, 1, "Info", null, null);
            logger.LogTrace("its ok");
            logger.LogCritical("aaaaa");
            logger.Log(LogLevel.None, 2, "adas");
            foreach (var i in Enumerable.Repeat("Disconnected. Reconnecting.", 1500))
            {
                logger.LogCritical(new Exception("asdasdasdsad\nlsmfkakf"), i);
            }
            logger.Dispose();
        }
    }
}