using RegexAiService;
using System.Collections.Generic;

namespace RegexAiServiceSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var models = new List<RegexAiModel>
            {
                new RegexAiModel()
                {
                    Intent = "Help",
                    Patterns = new List<string>()
                    {
                        "help"
                    }
                },
                new RegexAiModel()
                {
                    Intent = "HelpMe",
                    Patterns = new List<string>()
                    {
                        "help me"
                    }
                },
                new RegexAiModel()
                {
                    Intent = "Quit",
                    Patterns = new List<string>()
                    {
                        "(exit|quit)"
                    }
                },
                new RegexAiModel()
                {
                    Intent = "AskDate",
                    Patterns = new List<string>() {
                        "(?<DayDiff>今天|明天|後天|昨天|前天|大後天|大前天)是幾月幾日$",
                        "(?<DayDiff>今天|明天|後天|昨天|前天|大後天|大前天)幾月幾日$"
                    }
                }
            };

            var recognizer = new RegexAiRecognizer(models);

            var result = recognizer.Parse("今天是幾月幾日");
            var result2 = recognizer.Parse("請問一下，今天是幾月幾日");
            var result3 = recognizer.Parse("Help me please");
        }
    }
}
