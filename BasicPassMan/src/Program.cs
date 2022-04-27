using System;
using BasicPassMan.JSON;
using BasicPassMan.JSON.Builder;
using BasicPassMan.JSON.Writer;

namespace BasicPassMan
{
    class Program
    {
        static void Main(string[] args)
        {
            var userJson = JsonBuilder.CreateJsonUserObject();
            JsonWriter.WriteJson(userJson);
        }
    }
}