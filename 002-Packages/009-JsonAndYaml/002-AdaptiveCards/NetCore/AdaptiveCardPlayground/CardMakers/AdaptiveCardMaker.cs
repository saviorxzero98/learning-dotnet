using AdaptiveCards;
using AdaptiveCards.Rendering.Html;
using AdaptiveCards.Templating;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Text;

namespace AdaptiveCardPlayground.CardMakers
{
    public static class AdaptiveCardMaker
    {
        /// <summary>
        /// 讀取 Adaptive Card 檔案
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static AdaptiveCard ReadAdaptiveCardFile(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string cardJson = r.ReadToEnd();
                return CreateCard(cardJson);
            }
        }

        /// <summary>
        /// 讀取 Adaptive Card 範本檔案
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static AdaptiveCard ReadAdaptiveCardTemplateFile(string filePath, object data)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string templateJson = r.ReadToEnd();
                return CreateCardFromTemplate(templateJson, data);
            }
        }

        /// <summary>
        /// Adaptive Card 轉成 HTML 檔案
        /// </summary>
        /// <param name="card"></param>
        public static void WriteHtmlFile(string filePath, AdaptiveCard card)
        {
            string cardComponent = RenderToHtml(card);

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<html>");
            htmlBuilder.Append("<header>");
            htmlBuilder.Append("<link href=\"webchat-container.css\" rel=\"stylesheet\" />");
            htmlBuilder.Append("<link href=\"card.css\" rel=\"stylesheet\" />");
            htmlBuilder.Append("</header");
            htmlBuilder.Append("<body><div class=\"card\"><div class=\"container\">");
            htmlBuilder.Append(cardComponent);
            htmlBuilder.Append("</div></div></body>");
            htmlBuilder.Append("</html>");

            StreamWriter sw = new StreamWriter(filePath);
            sw.WriteLine(htmlBuilder.ToString());
            sw.Close();
        }


        /// <summary>
        /// 建立 Adaptive Card
        /// </summary>
        /// <param name="cardJson"></param>
        /// <returns></returns>
        public static AdaptiveCard CreateCard(string cardJson)
        {
            var parseResults = AdaptiveCard.FromJson(cardJson);

            if (!parseResults.Warnings.Any())
            {
                return parseResults.Card;
            }

            return default(AdaptiveCard);
        }

        /// <summary>
        /// 透過 Adaptive Card 範本建立 Adaptive Card
        /// </summary>
        /// <param name="templateJson"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static AdaptiveCard CreateCardFromTemplate(string templateJson, object data)
        {
            // 範本
            AdaptiveCardTemplate template = new AdaptiveCardTemplate(templateJson);

            // 資料
            JToken bindData = JToken.FromObject(data);


            string cardJson = template.Expand(bindData);
            return CreateCard(cardJson);
        }

        /// <summary>
        /// Adaptive Card 轉成 HTML
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static string RenderToHtml(AdaptiveCard card)
        {
            AdaptiveCardRenderer renderer = new AdaptiveCardRenderer();
            var result = renderer.RenderCard(card);
            return result.Html.ToString();
        }
    }
}
