using System.Collections.Generic;
using System.IO;
using BasicPassMan.UserUtils;
using Newtonsoft.Json.Linq;

namespace BasicPassMan.JSON.Builder
{
    public static class JsonBuilder
    {
        public static JObject CreateJsonUserObject(User cSharpUser)
        {
            if (new FileInfo(JsonFile.JsonPath).Length != 0) return null;
            var user = JObject.FromObject(new
            {
                User = new
                {
                    cSharpUser.Username,
                    cSharpUser.UserEmail,
                    cSharpUser.Accounts,
                    cSharpUser.Salt
                }
            });

            return user;
        }
    }
}