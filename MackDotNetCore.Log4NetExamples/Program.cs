using log4net;
using log4net.Config;
using System;
using System.Reflection;


ILog log = LogManager.GetLogger(typeof(Program));


Assembly assembly = Assembly.GetExecutingAssembly();
string projectName = assembly.GetName().Name!;

XmlConfigurator.Configure(new FileInfo("log4net.config"));

log.Info($"{projectName} - This is an info message");
log.Warn($"{projectName} - This is a warning message");

Console.WriteLine("Check the console for log messages. Press any key to exit.");
Console.ReadKey();



