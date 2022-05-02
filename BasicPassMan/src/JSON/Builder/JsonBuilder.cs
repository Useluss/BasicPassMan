using System.IO;
using BasicPassMan.UserUtils;
using Newtonsoft.Json.Linq;

namespace BasicPassMan.JSON.Builder
{
    public static class JsonBuilder
    {
        public static JObject CreateJsonUserObject(User user)
        {
            if (new FileInfo(JsonFile.JsonPath).Length != 0)
            {
                File.WriteAllText(JsonFile.JsonPath, string.Empty);
            }
            
            var jUser = JObject.FromObject(new
            {
                User = new
                {
                    user.Username,
                    user.UserEmail,
                    user.Accounts
                }
            });

            return jUser;
        }
    }
}