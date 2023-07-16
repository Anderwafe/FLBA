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