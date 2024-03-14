using AngleSharp.Dom;
using AngleSharp;

namespace AngelSharpSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //html代碼
            var source = @"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <meta property=""og:title"" content=""123"" />
    <title>ABC</title>
</head>
</html>
";

            IDocument document = BrowsingContext.New(Configuration.Default.WithDefaultLoader())
                                .OpenAsync(req => req.Content(source)).Result;

            var title = document.QuerySelector("meta[property=\"og:title\"]")?.Attributes["content"]?.Value;
            Console.WriteLine(title);
        }
    }
}
