using AdaptiveCardPlayground.CardMakers;
using AdaptiveCardPlayground.Models;
using AdaptiveCards;
using AdaptiveCards.Templating;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdaptiveCardPlayground
{
    class Program
    {
        static string TemplateFolder = "CardTemplates";
        static string ResultFolder = "CardResults";

        static void Main(string[] args)
        {
            DemoFlexMessage();
            DemoFlexGridCard();

            DemoAdaptiveCard();
            DemoAdaptiveCardWithGrid();

            DemoJustNet();
        }

        static void DemoAdaptiveCard()
        {
            var leaveBoyData = new ApplyLeaveForm()
            {
                Applicant = "張三",
                Sex = "男",
                Agents = new List<LeaveAgent>()
                {
                    new LeaveAgent("0004", "李四"),
                    new LeaveAgent("0005", "王五"),
                    new LeaveAgent("0006", "趙六")
                },
                LeaveType = "特休假",
                LeaveHours = 8,
                Subject = "特休"
            };
            var leaveGirlData = new ApplyLeaveForm()
            {
                Applicant = "張三",
                Sex = "女",
                Agents = new List<LeaveAgent>()
                {
                    new LeaveAgent("0004", "李四"),
                    new LeaveAgent("0005", "王五"),
                    new LeaveAgent("0006", "趙六")
                },
                LeaveType = "生理假",
                LeaveHours = 8,
                Subject = "大姨媽來了"
            };

            // 讀取 Adaptive Card 範本
            string inputFilePath = $"{TemplateFolder}\\ApplyLeaveCard.json";
            AdaptiveCard boyCard = AdaptiveCardMaker.ReadAdaptiveCardTemplateFile(inputFilePath, leaveBoyData);
            AdaptiveCard girlCard = AdaptiveCardMaker.ReadAdaptiveCardTemplateFile(inputFilePath, leaveGirlData);

            // 儲存成 HTML 檔案
            string outputFilePathForBoy = $"{ResultFolder}\\AdaptiveCard_Boy.html";
            AdaptiveCardMaker.WriteHtmlFile(outputFilePathForBoy, boyCard);
            Console.WriteLine("Create Card 'AdaptiveCard_Boy' Success.");

            string outputFilePathForGirl = $"{ResultFolder}\\AdaptiveCard_Girl.html";
            AdaptiveCardMaker.WriteHtmlFile(outputFilePathForGirl, girlCard);
            Console.WriteLine("Create Card 'AdaptiveCard_Girl' Success.");
        }

        static void DemoAdaptiveCardWithGrid()
        {
            var gridData = new GridData()
            {
                Cards = new List<GridItem>()
                {
                    new GridItem(1, "Ace", "黑桃"),
                    new GridItem(11, "Jack", "方塊"),
                    new GridItem(12, "Queen", "愛心"),
                    new GridItem(13, "King", "梅花"),
                    new GridItem(14, "Joke", "無")
                }
            };


            // 讀取 Adaptive Card 範本
            string inputFilePath = $"{TemplateFolder}\\GridCard.json";
            AdaptiveCard card = AdaptiveCardMaker.ReadAdaptiveCardTemplateFile(inputFilePath, gridData);

            // 儲存成 HTML 檔案
            string outputFilePath = $"{ResultFolder}\\AdaptiveCard_Grid.html";
            AdaptiveCardMaker.WriteHtmlFile(outputFilePath, card);
            Console.WriteLine("Create Card 'AdaptiveCard_Grid' Success.");
        }

        static void DemoJustNet()
        {
            var leaveData = new ApplyLeaveForm()
            {
                Applicant = "張三",
                Sex = "女",
                Agents = new List<LeaveAgent>()
                {
                    new LeaveAgent("0004", "李四"),
                    new LeaveAgent("0005", "王五"),
                    new LeaveAgent("0006", "趙六")
                },
                LeaveType = "特休假",
                LeaveHours = 8,
                StartDate = DateTime.Now.ToString("yyyy-MM-dd"),
                Subject = "特休"
            };

            // 讀取 Adaptive Card 範本
            string inputFilePath = $"{TemplateFolder}\\ApplyLeaveCard_JustNet.json";
            AdaptiveCard card = JustNetCardMaker.ReadAdaptiveCardTemplateFile(inputFilePath, leaveData);

            // 儲存成 HTML 檔案
            string outputFilePath = $"{ResultFolder}\\AdaptiveCard_JustNet.html";
            JustNetCardMaker.WriteHtmlFile(outputFilePath, card);
            Console.WriteLine("Create Card 'AdaptiveCard_JustNet' Success.");
        }
    
        static void DemoFlexMessage()
        {
            string inputFilePath = $"{TemplateFolder}\\LineFlexMessage.json";

            using (StreamReader r = new StreamReader(inputFilePath))
            {
                string templateJson = r.ReadToEnd();

                // 範本
                AdaptiveCardTemplate template = new AdaptiveCardTemplate(templateJson);

                // 資料
                JToken bindData = JToken.FromObject(new { 
                    Today = DateTime.Today.ToString("yyyy-MM-dd")
                });

                string cardJson = template.Expand(bindData);
            }
        }

        static void DemoFlexGridCard()
        {
            string inputFilePath = $"{TemplateFolder}\\LineFlexGridCard.json";

            using (StreamReader r = new StreamReader(inputFilePath))
            {
                string templateJson = r.ReadToEnd();

                // 範本
                AdaptiveCardTemplate template = new AdaptiveCardTemplate(templateJson);

                // 資料
                var gridData = new GridData()
                {
                    Cards = new List<GridItem>()
                    {
                        new GridItem(1, "Ace", "黑桃"),
                        new GridItem(11, "Jack", "方塊"),
                        new GridItem(12, "Queen", "愛心"),
                        new GridItem(13, "King", "梅花"),
                        new GridItem(14, "Joke", "無")
                    }
                };

                JToken bindData = JToken.FromObject(gridData);

                string cardJson = template.Expand(bindData);

                JToken card = JsonConvert.DeserializeObject<JToken>(cardJson);
                //cardJson = cardJson.Replace("},]", "}]");       // 修正轉換後的錯誤

                string outputFilePath = $"{ResultFolder}\\LineFlexGridCard.json";
                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    string outCardJson = JsonConvert.SerializeObject(card, Formatting.Indented);
                    writer.WriteLine(outCardJson);
                }
            }
        }
    }
}
