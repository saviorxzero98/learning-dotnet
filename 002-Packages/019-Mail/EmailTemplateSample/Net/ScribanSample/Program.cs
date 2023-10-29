using Scriban;

namespace ScribanSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DemoScibanRenderer();
            DemoLiquidRenderer();
        }

        static void DemoScibanRenderer()
        {
            var template = Template.Parse("Hello {{name}}! {{user_name}}");
            var result = template.Render(new { Name = "World", UserName = "Scriban" });
            Console.WriteLine(result);
        }

        static void DemoLiquidRenderer()
        {
            var template = Template.ParseLiquid("Hello {{name}}! {{user_name}}");
            var result = template.Render(new { Name = "World", UserName = "Liquid" });
            Console.WriteLine(result);
        }
    }
}