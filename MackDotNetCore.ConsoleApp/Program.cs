// See https://aka.ms/new-console-template for more information
using MackDotNetCore.ConsoleApp.EFCoreExamples;
using MackDotNetCore.ConsoleApp.HttpClientExamples;
using MackDotNetCore.ConsoleApp.RefitExamples;

//Console.WriteLine("Hello, World!");
//EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Run();
Console.WriteLine("Please wait for api...");
Console.ReadKey();


//HttpClientExample httpClientExample = new HttpClientExample();
//await httpClientExample.Run();
RefitExample refitExample = new RefitExample();
await refitExample.Run();

Console.ReadKey();
