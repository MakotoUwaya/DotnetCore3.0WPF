using System;
using Microsoft.Windows.EventTracing;

namespace ETWApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1 || string.IsNullOrWhiteSpace(args[0]))
            {
                Console.Error.WriteLine("Usage: <trace.etl>");
                return;
            }

            using (var trace = TraceProcessor.Create(args[0]))
            {
                var pendingProcessData = trace.UseProcesses();
                trace.Process();
                var processData = pendingProcessData.Result;

                foreach (var process in processData.Processes)
                {
                    Console.WriteLine(process.CommandLine);
                }
            }
        }
    }
}
