using System;
using System.IO;
using System.Text;
using BasicPassMan.JSON.Builder;
using BasicPassMan.JSON.Encryptor;
using BasicPassMan.JSON.Writer;
using BasicPassMan.UserUtils;
using DotNetEnv;

namespace BasicPassMan
{
    class Program
    {
        static void Main(string[] args)
        {
            var encryptor = new JsonFileEncrypt();

            var user = new User
            {
                Username = "Useluss", // Temp declaration 
                UserEmail = "ryanfiset256@gmail.com", // Temp declaration
                Accounts = null
            };
            Console.WriteLine("Please enter a password: ");
            var password = Console.ReadLine() + user.Username + user.UserEmail;
            
            var userJson = JsonBuilder.CreateJsonUserObject(user);
            JsonWriter.WriteJson(userJson);
            
            encryptor.Encrypt(null, ref user, Encoding.ASCII.GetBytes(password));
        }
    }
}