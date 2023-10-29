using Fluid;

namespace FluidSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Demo();
            DemoSnakeCase();
        }

        static void Demo()
        {
            var parser = new FluidParser();

            var model = new { Name = "World", UserName = "Liquid" };
            var source = "Hello {{ Name }}! {{ UserName }}";

            if (parser.TryParse(source, out var template, out var error))
            {
                var context = new TemplateContext(model);

                Console.WriteLine(template.Render(context));
            }
            else
            {
                Console.WriteLine($"Error: {error}");
            }
        }

        static void DemoSnakeCase()
        {
            var parser = new FluidParser();

            var model = new { Name = "World", UserName = "Snake" };
            var source = "Hello {{ name }}! {{ user_name }}";

            if (parser.TryParse(source, out var template, out var error))
            {
                var options = new TemplateOptions();
                options.MemberAccessStrategy.MemberNameStrategy = MemberNameStrategies.SnakeCase;

                var context = new TemplateContext(model, options);

                Console.WriteLine(template.Render(context));
            }
            else
            {
                Console.WriteLine($"Error: {error}");
            }
        }
    }
}