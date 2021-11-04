using System;
using Microsoft.Extensions.Logging;

namespace SettingExtend.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //https://docs.microsoft.com/zh-cn/dotnet/core/extensions/logging?tabs=command-line#non-host-console-app
            var logger = Log.Instance().CreateLogger<Program>();
            logger.LogInformation("abc");
            var setting = Utility.Parse("Text1.txt");
            Console.ReadKey();
        }
    }
}