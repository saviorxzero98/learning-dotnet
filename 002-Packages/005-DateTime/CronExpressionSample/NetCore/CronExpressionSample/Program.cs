using NCrontab;
using NCrontabAdv = NCrontab.Advanced;

namespace CronExpressionSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var validCronList = new List<string>()
            {
                "5 * * * ?",
                "5 * * * *",
                "5 * * * * ?",
                "5 * * * * *",
                "5 * * * * * 2022",

                "40/59 * * * *",
                "40/59 * * * ?",
                "40/59 * * * * *",
                "40/59 * * * * ?"
            };
            var invalidCronList = new List<string>()
            {
                "40/60 * * * *",
                "40/60 * * * ?",
                "40/60 * * * * *",
                "40/60 * * * * ?",

                "1/10 40/60 * * * *"
            };

            foreach (var cron in validCronList)
            {
                Console.WriteLine($"Vaild Cron: {cron}");

                DemoNCrontab(cron);
                DemoNCrontabAdvanced(cron);
                DemoCronos(cron);
                DemoQuartzNET(cron);

                Console.WriteLine("\n");
            }

            foreach (var cron in invalidCronList)
            {
                Console.WriteLine($"Invalid Cron: {cron}");

                DemoNCrontab(cron);
                DemoNCrontabAdvanced(cron);
                DemoCronos(cron);
                DemoQuartzNET(cron);

                Console.WriteLine("\n");
            }
        }

        static void DemoNCrontab(string cron)
        {
            string packageName = "NCrontab";

            try
            {
                var options = new CrontabSchedule.ParseOptions()
                {
                    IncludingSeconds = (GetCronFieldCount(cron) != 5)
                };
                CrontabSchedule schedule = CrontabSchedule.Parse(cron, options);

                if (schedule != null)
                {
                    var nextDate = schedule.GetNextOccurrence(DateTime.Now);
                    PrintSuccessResult(packageName, nextDate);
                }
                else
                {
                    PrintFailResult(packageName);
                }
            }
            catch (Exception e)
            {
                PrintFailResult(packageName, e.Message);
            }
        }

        static void DemoNCrontabAdvanced(string cron)
        {
            string packageName = "NCrontabAdv";

            try
            {
                var format = NCrontabAdv.Enumerations.CronStringFormat.Default;
                int fieldCount = GetCronFieldCount(cron);
                switch (fieldCount)
                {
                    case 6:
                        format = NCrontabAdv.Enumerations.CronStringFormat.WithSeconds;
                        break;
                    case 7:
                        format = NCrontabAdv.Enumerations.CronStringFormat.WithSecondsAndYears;
                        break;
                }
                var schedule = NCrontabAdv.CrontabSchedule.Parse(cron, format);

                if (schedule != null)
                {
                    var nextDate = schedule.GetNextOccurrence(DateTime.Now);
                    PrintSuccessResult(packageName, nextDate);
                }
                else
                {
                    PrintFailResult(packageName);
                }
            }
            catch (Exception e)
            {
                PrintFailResult(packageName, e.Message);
            }
        }

        static void DemoCronos(string cron)
        {
            string packageName = "Cronos";

            try
            {
                var format = Cronos.CronFormat.Standard;
                if (GetCronFieldCount(cron) == 6)
                {
                    format = Cronos.CronFormat.IncludeSeconds;
                }
                var expression = Cronos.CronExpression.Parse(cron, format);

                if (expression != null)
                {
                    var nextDate = expression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Utc);
                    PrintSuccessResult(packageName, nextDate);
                }
                else
                {
                    PrintFailResult(packageName);
                }
            }
            catch (Exception e)
            {
                PrintFailResult(packageName, e.Message);
            }
        }

        static void DemoQuartzNET(string cron)
        {
            string packageName = "Quartz";

            try
            {
                Quartz.CronExpression expression = new Quartz.CronExpression(cron);
                var nextDate = expression.GetNextValidTimeAfter(DateTimeOffset.Now);
                PrintSuccessResult(packageName, nextDate);
            }
            catch (Exception e)
            {
                PrintFailResult(packageName, e.Message);
            }
        }


        static int GetCronFieldCount(string cron)
        {
            if (string.IsNullOrWhiteSpace(cron))
            {
                return 0;
            }

            return (cron.Trim()
                        .Split(' ')
                        .Where(c => !string.IsNullOrEmpty(c))
                        .Count());
        }

        static void PrintSuccessResult(string packageName, DateTime? nextDate)
        {
            if (nextDate == null)
            {
                PrintFailResult(packageName);
                return;
            }
            else
            {
                Console.WriteLine($"[{packageName}] Success; Next Date: {nextDate?.ToString("yyyy-MM-dd HH:mm:ss")}");
            }
        }
        static void PrintSuccessResult(string packageName, DateTimeOffset? nextDate)
        {
            if (nextDate == null)
            {
                PrintFailResult(packageName);
                return;
            }
            else
            {
                var date = (DateTimeOffset)nextDate;
                Console.WriteLine($"[{packageName}] Success; Next Date: {date.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss")}");
            }
        }
        static void PrintFailResult(string packageName)
        {
            Console.WriteLine($"[{packageName}] Fail");
        }
        static void PrintFailResult(string packageName, string errorMessage)
        {
            Console.WriteLine($"[{packageName}] Fail; {errorMessage}");
        }
    }
}