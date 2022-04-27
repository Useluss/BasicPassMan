using System.Collections.Generic;
using System.IO;
using BasicPassMan.UserUtils;
using Newtonsoft.Json.Linq;

namespace BasicPassMan.JSON.Builder
{
    public class JsonBuilder
    {
        public static JObject CreateJsonUserObject()
        {
            if (new FileInfo(JsonFile.JsonPath).Length != 0) return null;
            var user = JObject.FromObject(new
            {
                User = new
                {
                    Username = "Useluss",
                    Accounts = new List<Account>()
                }
            });

            return user;

        }
    }
}