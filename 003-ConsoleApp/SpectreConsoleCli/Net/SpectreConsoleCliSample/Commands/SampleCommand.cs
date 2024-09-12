using Spectre.Console;
using Spectre.Console.Cli;
using SpectreConsoleCliSample.Settings;

namespace SpectreConsoleCliSample.Commands
{
    public class SampleCommand : Command<SampleSettings>
    {
        public override int Execute(CommandContext context, SampleSettings settings)
        {
            if (!string.IsNullOrEmpty(settings.FilePath))
            {
                var path = new TextPath(settings.FilePath)
                    .RootStyle(new Style(foreground: Color.Red))
                    .SeparatorStyle(new Style(foreground: Color.Green))
                    .StemStyle(new Style(foreground: Color.Blue))
                    .LeafStyle(new Style(foreground: Color.Yellow));
                var panel = new Panel(path);
                panel.Header = new PanelHeader("目錄");
                AnsiConsole.Write(panel);
                Console.WriteLine();
            }

            if (!string.IsNullOrEmpty(settings.Date) && DateTime.TryParse(settings.Date, out DateTime date))
            {
                var calendar = new Calendar(date).Culture("zh-TW");
                var panel = new Panel(calendar);
                panel.Header = new PanelHeader("日期");
                AnsiConsole.Write(panel);
            }
            return 0;
        }
    }
}