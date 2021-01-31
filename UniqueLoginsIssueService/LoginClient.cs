using System;
using System.Threading;

namespace UniqueLoginsIssueService
{
    public class LoginClient
    {
        public string LogIn(string login, string password)
        {
            Random random = new Random();
            double success = random.NextDouble();
            double sleep = random.NextDouble();

            Thread.Sleep(Convert.ToInt32(sleep * 1000));

            if (success < 0.5)
                return Guid.NewGuid().ToString();
            return null;
        }
    }
}
