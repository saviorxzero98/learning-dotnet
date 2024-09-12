using Spectre.Console.Cli;

namespace SpectreConsoleCliSample.Settings
{
    public class SampleSettings : CommandSettings
    {
        [CommandArgument(0, "[filepath]")]
        public string? FilePath { get; set; }

        [CommandOption("-d|--data")]
        public string? Date { get; set; }
    }
}
