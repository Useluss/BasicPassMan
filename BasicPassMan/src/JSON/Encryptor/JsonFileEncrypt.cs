using System;
using System.IO;
using System.Security.Cryptography;
using BasicPassMan.JSON.Writer;
using BasicPassMan.UserUtils;
using DotNetEnv;
using Microsoft.VisualBasic;

namespace BasicPassMan.JSON.Encryptor
{
    public class JsonFileEncrypt : JsonFile
    {
        // Size in bytes
        private const int SaltSize = 32;
        private const int HashSize = 32;
        private const int Iterations = 100000; // Number of PBKDF2 iterations

        public void Encrypt(byte[] key, ref User user, byte[] password) // Password is temporary
        {
            if (key == null)
            {
                byte[] salt = SaltGenerator();
                
                using (var sourceStream = File.OpenRead(JsonPath))
                using(var destinationStream = File.Create(EncryptedPath))
                using (var provider = new AesCryptoServiceProvider())
                using (var cryptoTransfer = provider.CreateEncryptor())
                using (var cryptoStream = new CryptoStream(destinationStream, cryptoTransfer, CryptoStreamMode.Write))
                {
                    provider.Key = CreateKey(salt, password);
                    Env.Load();
                    using (var writer = new StreamWriter((@"C:\Users\ryanf\RiderProjects\BasicPassMan\.env")))
                    {
                        writer.WriteLine("SECRET_KEY=" + Convert.ToBase64String(provider.Key));
                        writer.WriteLine("SALT=" + Convert.ToBase64String(salt));
                    }
                    
                    destinationStream.Write(provider.IV, 0, provider.IV.Length);
                    sourceStream.CopyTo(cryptoStream);

                    Env.Load(Path.GetFullPath(@"C:\Users\ryanf\RiderProjects\BasicPassMan\.env"));
                    Console.WriteLine(Environment.GetEnvironmentVariable("SECRET_KEY"));
                }
            }
            else
            {
                // Decrypt the source file and write it to the json file that was originally encrypted.
                using (var sourceStream = File.OpenRead(EncryptedPath))
                using (var destinationStream = File.Create(JsonPath))
                using (var provider = new AesCryptoServiceProvider())
                {
                    var iv = new byte[provider.IV.Length];
                    sourceStream.Read(iv, 0, iv.Length);
                    using (var cryptoTransform = provider.CreateDecryptor(key, iv))
                    using (var cryptoStream = new CryptoStream(sourceStream, cryptoTransform, CryptoStreamMode.Read))
                    {
                        cryptoStream.CopyTo(destinationStream);
                    }
                }
            }
        }

        private static byte[] SaltGenerator()
        {
            var salt = new byte[SaltSize];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            return salt;
        }

        public static byte[] CreateKey(byte[] salt, byte[] password)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(HashSize);
        }
    }
}