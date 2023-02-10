using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexAiService
{
    public class RegexAiRecognizer
    {
        protected const string EntityNamePattern = "[^0-9\\W]";
        protected const string WordPatten = "\\b[\\w']*\\b";

        public List<RegexAiModel> Models { get; set; }

        public RegexAiRecognizer(List<RegexAiModel> models)
        {
            Models = models ?? new List<RegexAiModel>();
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public RegexAiResult Parse(string text)
        {
            var intents = new List<RegexAiIntent>();

            foreach (var model in Models)
            {
                // Get Regex Pattern
                var pattern = string.Join("|", model.Patterns);
                var regex = (model.IgnoreCase) ? new Regex(pattern, RegexOptions.IgnoreCase) : new Regex(pattern);

                // Parse Intent
                if (regex.IsMatch(text))
                {
                    // Match Regex Groups
                    Match match = regex.Match(text);

                    // Get Intent
                    double score = CalculateScore(text, match.Value);
                    var intent = new RegexAiIntent()
                    {
                        Intent = model.Intent,
                        Pattern = pattern,
                        Score = score
                    };

                    // Get Entities
                    intent.Entities = GetEntities(regex.GetGroupNames().ToList(), match.Groups);

                    // Add Intents
                    intents.Add(intent);
                }
            }

            // 取得 Top Intent
            RegexAiIntent topIntent = new RegexAiIntent();
            if (intents.Any())
            {
                intents = intents.OrderByDescending(i => i.Score).ToList();
                topIntent = intents.FirstOrDefault();
            }

            // 建立 Results
            RegexAiResult result = new RegexAiResult()
            {
                Query = text,
                TopIntent = topIntent,
                Intents = intents
            };
            return result;
        }

        /// <summary>
        /// Get Entities
        /// </summary>
        /// <param name="groupNames"></param>
        /// <param name="groups"></param>
        /// <returns></returns>
        protected List<RegexAiEntity> GetEntities(List<string> groupNames, GroupCollection groups)
        {
            var entities = new List<RegexAiEntity>();

            foreach (var groupName in groupNames)
            {
                if (Regex.IsMatch(groupName, EntityNamePattern))
                {
                    var entity = new RegexAiEntity()
                    {
                        Entity = groupName,
                        Value = groups[groupName].Value,
                        Start = groups[groupName].Index,
                        End = groups[groupName].Index + groups[groupName].Length
                    };

                    entities.Add(entity);
                }
            }

            return entities;
        }

        /// <summary>
        /// 計算 Intent 分數
        /// </summary>
        /// <param name="text"></param>
        /// <param name="matchedText"></param>
        /// <returns></returns>
        protected double CalculateScore(string text, string matchedText)
        {
            // 取得字數
            Regex regex = new Regex(WordPatten);
            var textWords = GetWords(text);
            var matchedWords = GetWords(matchedText);
            
            // 計算分數
            double initialScore = (double)matchedWords.Count / (double)textWords.Count;

            // 分數加權
            double score = 0.4 + (0.6 * initialScore);
            return score;
        }

        /// <summary>
        /// 取得 Word
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected List<string> GetWords(string text)
        {
            Regex regex = new Regex(WordPatten);
            var words = regex.Matches(text)
                             .Cast<Match>()
                             .Select(m => m.Value)
                             .Where(w => !string.IsNullOrWhiteSpace(w))
                             .ToList();
            return words;
        }
    }
}
