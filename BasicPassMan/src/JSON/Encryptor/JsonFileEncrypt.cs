using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using BasicPassMan.UserUtils;
using DotNetEnv;

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
                Environment.SetEnvironmentVariable("SALT", Convert.ToBase64String(salt), EnvironmentVariableTarget.User);
                
                using (var sourceStream = File.OpenRead(JsonPath))
                using(var destinationStream = File.Create(EncryptedPath))
                using (var provider = new AesCryptoServiceProvider())
                using (var cryptoTransfer = provider.CreateEncryptor())
                using (var cryptoStream = new CryptoStream(destinationStream, cryptoTransfer, CryptoStreamMode.Write))
                {
                    provider.Key = CreateKey(salt, password);
                    Environment.SetEnvironmentVariable("SECRET_KEY", Convert.ToBase64String(provider.Key), EnvironmentVariableTarget.User);
                    
                    destinationStream.Write(provider.IV, 0, provider.IV.Length);
                    sourceStream.CopyTo(cryptoStream);

                    Env.Load();
                    Console.WriteLine(Environment.GetEnvironmentVariable("SECRET_KEY"));
                }
            }
            else
            {
                // Decrypt the source file and write it to the json file that was originally encrypted.
                Console.WriteLine(Encoding.ASCII.GetByteCount(Convert.ToBase64String(key)));
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