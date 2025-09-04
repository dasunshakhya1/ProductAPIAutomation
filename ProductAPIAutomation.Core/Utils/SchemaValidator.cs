using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace ProductAPIAutomation.Core.Utils
{
    public class SchemaValidator
    {

        public static bool IsValidSchema(string schemaJson, string payload)
        {
            JSchema schema = JSchema.Parse(schemaJson);
            JToken json = JToken.Parse(payload);
            bool valid = json.IsValid(schema, out IList<string> messages);

            if (valid)
            {
                return true;
            }
            Console.WriteLine("JSON is NOT valid. Errors:");
            foreach (string message in messages)
            {
                Console.WriteLine($"- {message}");
            }
            return false;
        }
    }
}
