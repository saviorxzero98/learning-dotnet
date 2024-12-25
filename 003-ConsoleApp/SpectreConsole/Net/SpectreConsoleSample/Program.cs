using Spectre.Console;

namespace SpectreConsoleSample
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            DemoMarkup();

            await DemoProgressAsync();

            await DemoStatusAsync();

            DemoPrompt();
        }

        // Markup
        static void DemoMarkup()
        {
            AnsiConsole.MarkupLine("[bold]===== Markup =====[/]");
            AnsiConsole.Markup("[blue]Blue[/] ");
            AnsiConsole.Markup("[green]Green[/] ");
            AnsiConsole.Markup("[red]Red[/] ");
            AnsiConsole.Markup("[black on white] Back [/] ");
            AnsiConsole.MarkupLine("\n[bold]====================[/]");
            Console.WriteLine();
        }

        // Progress
        static async Task DemoProgressAsync()
        {
            AnsiConsole.MarkupLine("[bold]===== Progress =====[/]");
            await AnsiConsole.Progress()
                .StartAsync(async ctx =>
                {
                    // Define tasks
                    var task1 = ctx.AddTask("[green]讀取資料[/]");
                    var task2 = ctx.AddTask("[green]寫入資料[/]");

                    while (!ctx.IsFinished)
                    {
                        await Task.Delay(250);
                        task1.Increment(10);
                        task2.Increment(5);
                    }
                });
            AnsiConsole.MarkupLine("[bold]====================[/]");
            Console.WriteLine();
        }

        static async Task DemoStatusAsync()
        {
            AnsiConsole.MarkupLine("[bold]===== Status =====[/]");
            await AnsiConsole.Status()
                .StartAsync("資料轉擋中", async ctx =>
                {
                    Thread.Sleep(3000);
                });
            AnsiConsole.MarkupLine("[green]資料轉擋完成[/]");
            AnsiConsole.MarkupLine("[bold]====================[/]");
            Console.WriteLine();
        }

        // Prompt
        static void DemoPrompt()
        {
            AnsiConsole.MarkupLine("[bold]===== Prompt =====[/]");

            var userName = AnsiConsole.Prompt(new TextPrompt<string>("請輸入你的大名?"));
            var age = AnsiConsole.Prompt(
                new TextPrompt<int>("請輸入你的年齡?")
                    .Validate(value => (value <= 0) ? ValidationResult.Error("年齡不得小於0") : ValidationResult.Success())
                    .ValidationErrorMessage("請輸入正整數"));
            var playerClass = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("請輸入你的職業?")
                    .AddChoices(new string[] {
                        "戰士", "騎士", "法師", "弓箭手", "僧侶"
                    }));
            AnsiConsole.MarkupLine($"你選擇了 [bold yellow]{playerClass}[/]");
            var password = AnsiConsole.Prompt(new TextPrompt<string>("請輸入你的密碼").Secret());

            var equipments = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("請選擇初始裝備")
                    .NotRequired() // Not required to have a favorite fruit
                    .PageSize(10)
                    .InstructionsText(
                        "[grey](請按 [blue]<space>[/] 選擇裝備，按下 [green]<enter>[/] 完成選擇)[/]")
                    .AddChoiceGroup("武器", new string[] { "長劍", "長槍", "巨斧", "法杖", "魔導書", "長弓", "短劍", "巨錘" })
                    .AddChoiceGroup("防具", new string[] { "鐵鎧", "圓盾", "長袍", "皮甲", "修道服" })
                    .AddChoices(new string[] { "戒指", "耳環", "項鍊" })
                    .Select("長劍"));
            AnsiConsole.MarkupLine($"你選擇了裝備 [bold yellow]{string.Join("、", equipments)}[/]");

            Console.WriteLine();

            var grid = new Grid();
            grid.AddColumn();
            grid.AddColumn();
            grid.AddRow(new Text("姓名", new Style(Color.Yellow)), new Text(userName));
            grid.AddRow(new Text("年紀", new Style(Color.Yellow)), new Text(age.ToString()));
            grid.AddRow(new Text("職業", new Style(Color.Yellow)), new Text(playerClass));
            grid.AddRow(new Text("密碼", new Style(Color.Yellow)), new Text(password));
            AnsiConsole.Write(grid);

            var confirmation = AnsiConsole.Prompt(
                new TextPrompt<bool>("資料是否正確?")
                    .AddChoice(true)
                    .AddChoice(false)
                    .DefaultValue(true)
                    .WithConverter(choice => choice ? "y" : "n")
                    .InvalidChoiceMessage("是的話請輸入 [bold]y[/]；不是的話請輸入 [bold]n[/]"));

            AnsiConsole.MarkupLine("[bold]====================[/]");
            Console.WriteLine();
        }
    
    }
}
