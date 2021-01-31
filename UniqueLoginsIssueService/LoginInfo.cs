using System;

namespace UniqueLoginsIssueService
{
    public class LoginInfo
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public LoginInfo(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
