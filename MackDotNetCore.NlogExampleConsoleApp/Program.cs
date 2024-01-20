using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;


internal class Program
{
    private static void Main()
    {
        var logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<Program>();
        logger.LogInformation("Program has started.");
        Console.ReadKey();
    }
}
