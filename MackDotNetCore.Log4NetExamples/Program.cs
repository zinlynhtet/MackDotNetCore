using log4net;
using log4net.Config;
using System;
using System.Reflection;

namespace Log4netExample
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            // Get the name of the currently executing assembly
            Assembly assembly = Assembly.GetExecutingAssembly();
            string projectName = assembly.GetName().Name!;

            // Configure log4net using the XML configuration file
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));

            // Log some messages with the project name
            log.Info($"{projectName} - This is an info message");
            log.Warn($"{projectName} - This is a warning message");

            Console.WriteLine("Check the console for log messages. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
