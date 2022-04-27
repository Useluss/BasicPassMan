using System.Collections.Generic;
using System.Reflection.Metadata;

namespace BasicPassMan.UserUtils
{
    public class User
    {
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public List<Account> Accounts { get; set; }
    }
}