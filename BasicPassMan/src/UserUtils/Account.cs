using System.Security.Cryptography.X509Certificates;

namespace BasicPassMan.UserUtils
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string AdditionalInfo { get; set; }
    }
}