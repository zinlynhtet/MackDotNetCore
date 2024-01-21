using log4net;
using log4net.Config;
using System;

namespace MackDotNetCore.Log4NetExamples
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            // Configure log4net using the XML configuration file
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            // Log some messages
            log.Debug("This is a debug message");
            log.Info("This is an info message");
            log.Warn("This is a warning message");
            log.Error("This is an error message");
            log.Fatal("This is a fatal error message");

            Console.WriteLine("Check the console for log messages. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
