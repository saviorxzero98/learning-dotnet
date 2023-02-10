using System.Collections.Generic;

namespace RegexAiService
{
    public class RegexAiResult
    {
        public string Query { get; set; } = string.Empty;

        public RegexAiIntent TopIntent { get; set; } = new RegexAiIntent();

        public List<RegexAiIntent> Intents { get; set; } = new List<RegexAiIntent>();
    }

    public class RegexAiIntent
    {
        public const string NoneIntent = "None";

        public string Intent { get; set; } = NoneIntent;

        public List<RegexAiEntity> Entities { get; set; } = new List<RegexAiEntity>();

        public string Pattern { get; set; } = string.Empty;

        public double Score { get; set; } = 0;
    }

    public class RegexAiEntity
    {
        public string Entity { get; set; }

        public string Value { get; set; }

        public int Start { get; set; }

        public int End { get; set; }
    }
}
