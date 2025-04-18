using NJsonSchema;

namespace NJsonSchemaSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestDemo();
        }

        static void TestDemo()
        {
            string jsonInput = @"{
                'name': 'John Doe',
                'age': 30,
                'address': {
                    'street': '123 Main St',
                    'city': 'Anytown',
                    'state': 'CA',
                    'zip': '12345'
                },
                'phones': [
                    {
                        'type': 'home',
                        'number': '555-555-5555'
                    },
                    {
                        'type': 'work',
                        'number': '555-555-6666'
                    },
                    {
                        'type': 1
                    }
                ]
            }";

            try
            {
                // 生成 JSON Schema
                JsonSchema schema = JsonSchema.FromSampleJson(jsonInput);
                schema.Title = "Generated Schema";
                schema.Description = "Schema generated from input JSON";

                // 將 JSON Schema 轉換為字串
                string jsonSchemaString = schema.ToJson();
                Console.WriteLine(jsonSchemaString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
