using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace FLBA
{
    public static class FileLoggerExtensions
    {
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filePath = null)
        {
            filePath ??= Path.Join(Environment.CurrentDirectory, ".log");
            builder.AddProvider(new FileLoggerProvider(filePath));
            return builder;
        }
    }
}