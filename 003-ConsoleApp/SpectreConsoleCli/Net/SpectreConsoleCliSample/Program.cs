using Spectre.Console.Cli;
using SpectreConsoleCliSample.Commands;

namespace SpectreConsoleCliSample
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var commandApp = new CommandApp<SampleCommand>();
            return commandApp.Run(args);
        }
    }
}
