using System.IO;
using BasicPassMan.UserUtils;
using Newtonsoft.Json.Linq;

namespace BasicPassMan.JSON.Reader
{
    public static class JsonReader
    {
        public static User Read()
        {
            var jUser = JObject.Parse(File.ReadAllText(JsonFile.JsonPath));
            return ConvertToUser(jUser);
        }

        private static User ConvertToUser(JToken jUser)
        {
            var user = jUser.ToObject<User>();
            return user;
        }
    }
}