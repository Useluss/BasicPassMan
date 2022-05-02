using System;
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

            string salt = "";
            Env.Load();
            Environment.GetEnvironmentVariable("SALT");
            
            var user = new User
            {
                Username = "Useluss", // Temp declaration 
                UserEmail = "ryanfiset256@gmail.com", // Temp declaration
                Accounts = null
            };
            Console.WriteLine("Please enter a password: ");
            var password = Console.ReadLine() + user.Username + user.UserEmail + salt;

           encryptor.Encrypt(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY")), ref user, Encoding.ASCII.GetBytes(password));

            var userJson = JsonBuilder.CreateJsonUserObject(user);
            // JsonWriter.WriteJson(userJson);
        }
    }
}