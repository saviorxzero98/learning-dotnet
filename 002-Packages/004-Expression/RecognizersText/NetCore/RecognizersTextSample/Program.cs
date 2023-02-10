using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.DateTime;
using Microsoft.Recognizers.Text.Number;
using Microsoft.Recognizers.Text.NumberWithUnit;
using OpenCC.NET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecognizersTextSample
{
    class Program
    {
        static OpenChineseConverter Converter;

        static void Main(string[] args)
        {
            Converter = new OpenChineseConverter();

            DemoRecognizeNumber();
           
            DemoRecognizeNumberPercentage();

            DemoRecognizeNumberWithUnit();

            DemoRecoginzeDateTime();
        }

        static void DemoRecognizeNumber()
        {
            Console.WriteLine("===== Recognize Number =====");

            const string culture = Culture.Chinese;
            List<string> messages = new List<string>()
            {
                "柒佰陸拾壹億",
                "兩百",
                "速限一百一公里",
                "零點五",
                "玖點貳",
                "負肆拾參",
                "負肆拾三",
                "689",
                "６８９",
                "one million",
                "one",
                "two"
            };

            var recognizer = new NumberRecognizer(culture);
            var model = recognizer.GetNumberModel();

            foreach (var message in messages)
            {
                var scMessage = Converter.ToSimplifiedFromTaiwanWithPhrases(message);
                var results = model.Parse(scMessage);

                if (results.Any() && results.FirstOrDefault().Resolution.ContainsKey("value"))
                {
                    var value = results.FirstOrDefault().Resolution["value"];
                    Console.WriteLine($"{message} --> {value}");
                }
                else
                {
                    Console.WriteLine($"{message} --> X");
                }
            }
            Console.WriteLine("\n");
        }

        static void DemoRecognizeNumberPercentage()
        {
            Console.WriteLine("===== Recognize Number Percentage =====");

            const string culture = Culture.Chinese;
            List<string> messages = new List<string>()
            {
                "中獎機率百分之一",
                "致死率百分之二點一",
                "民調九點二趴"
            };

            var recognizer = new NumberRecognizer(culture);
            var model = recognizer.GetPercentageModel();

            foreach (var message in messages)
            {
                var scMessage = Converter.ToSimplifiedFromTaiwanWithPhrases(message);
                var results = model.Parse(scMessage);

                if (results.Any() && results.FirstOrDefault().Resolution.ContainsKey("value"))
                {
                    var value = results.FirstOrDefault().Resolution["value"];
                    Console.WriteLine($"{message} --> {value}");
                }
                else
                {
                    Console.WriteLine($"{message} --> X");
                }
            }
            Console.WriteLine("\n");
        }

        static void DemoRecognizeNumberWithUnit()
        {
            Console.WriteLine("===== Recognize With Unit =====");

            const string culture = Culture.Chinese;
            Dictionary<string, string> messages = new Dictionary<string, string>()
            {
                { "Age", "今年五十歲" },
                { "Dimension", "五奈米製程" },
                { "Temperature", "今天氣溫負四十三度" },
                { "Temperature2", "今天氣溫零下四十三度" },
                { "Currencie", "新台幣兩萬二" },
            };

            var recognizer = new NumberWithUnitRecognizer(culture);
            var keys = messages.Keys;

            foreach (var key in keys)
            {
                var message = messages[key];
                AbstractNumberWithUnitModel model = null;
                switch (key)
                {
                    case "Age":
                        model = recognizer.GetAgeModel();
                        break;
                    case "Dimension":
                        model = recognizer.GetDimensionModel();
                        break;
                    case "Temperature":
                    case "Temperature2":
                        model = recognizer.GetTemperatureModel();
                        break;
                    case "Currencie":
                        model = recognizer.GetCurrencyModel();
                        break;
                }

                if (model != null)
                {
                    var scMessage = Converter.ToSimplifiedFromTaiwanWithPhrases(message);
                    var results = model.Parse(scMessage);

                    if (results.Any() && results.FirstOrDefault().Resolution.ContainsKey("value"))
                    {
                        var value = results.FirstOrDefault().Resolution["value"];
                        var unit = results.FirstOrDefault().Resolution["unit"];
                        Console.WriteLine($"{message} --> {value} {unit}");
                    }
                    else
                    {
                        Console.WriteLine($"{message} --> X");
                    }
                }
            }
            Console.WriteLine("\n");
        }

        static void DemoRecoginzeDateTime()
        {
            Console.WriteLine("===== Recognize DateTime =====");

            const string culture = Culture.Chinese;
            List<string> messages = new List<string>()
            {
                "明天",
                "8月8日",
                "今天是10月10日",
                "目前時間是9點50分",
                "1912年10月10日",
                "民國100年10月10日",
                "下午9點50分",
                "下午9時50分",
                "9時50分30秒",
                "9點50分30秒",
                "下禮拜六",
                "下個禮拜六",
                "下個月19日",
                "去年聖誕節",
                "大後天",
                "October 10",
                "20201010"
            };

            var recognizer = new DateTimeRecognizer(culture);
            var model = recognizer.GetDateTimeModel();
            
            foreach (var message in messages)
            {
                var scMessage = Converter.ToSimplifiedFromTaiwanWithPhrases(message);
                var results = model.Parse(scMessage);

                if (results.Any() && 
                    results.FirstOrDefault().Resolution.ContainsKey("values"))
                {
                    var values = results.FirstOrDefault().Resolution["values"] as List<Dictionary<string, string>>;

                    if (values.Any())
                    {
                        var valueResults = new List<string>();
                        foreach (var value in values)
                        {
                            if (value.ContainsKey("type"))
                            {
                                if (value.ContainsKey("value"))
                                {
                                    valueResults.Add(value["value"]);
                                }
                                else if (value.ContainsKey("start") && value.ContainsKey("end"))
                                {
                                    valueResults.Add($"{value["start"]} ~ {value["end"]}");
                                }
                            }
                        }

                        if (valueResults.Any())
                        {
                            var value = string.Join(", ", valueResults);
                            Console.WriteLine($"{message} --> {value}");
                        }
                        else
                        {
                            Console.WriteLine($"{message} --> X");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{message} --> X");
                    }
                }
                else
                {
                    Console.WriteLine($"{message} --> X");
                }
            }
            Console.WriteLine("\n");
        }
    }
}
