using System;
using BasicPassMan.JSON.Builder;

namespace BasicPassMan
{
    class Program
    {
        static void Main(string[] args)
        {
            var userJson = JsonBuilder.CreateJsonUserObject();
        }
    }
}