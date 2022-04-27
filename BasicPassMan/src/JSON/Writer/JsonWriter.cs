using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BasicPassMan.JSON.Writer
{
    public class JsonWriter
    {
        public static void WriteJson(JObject user)
        {
            var serializer = new JsonSerializer();
            
            using (var sw = new StreamWriter(JsonFile.JsonPath))
            using (var writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, user);
            }
        }
    }
}