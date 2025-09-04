using System.Text.Json;

namespace ProductAPIAutomation.Core.Utils
{
    public class JsonParser
    {


        public static T ParseJson<T>( string json)
        {
            return JsonSerializer.Deserialize<T>(json);

        }

        public static string SerializeJson<T>(T obj) { 
            return JsonSerializer.Serialize(obj);
        }
    }
}