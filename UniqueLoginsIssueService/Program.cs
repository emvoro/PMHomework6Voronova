using System;
using System.Threading;
using System.Collections.Concurrent;
using System.IO;

namespace UniqueLoginsIssueService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Service of unique logins issue.\n Voronova Emilia\n");
            ConcurrentQueue<LoginInfo> loginInfo = new ConcurrentQueue<LoginInfo>();

            try
            {
                var logins = File.ReadAllLines("logins.csv");
                int threadCount = 0;
                Console.WriteLine(" Enter count of threads");

                while (!int.TryParse(Console.ReadLine(), out threadCount) || threadCount < 1 || threadCount > logins.Length)
                    Console.WriteLine(" Invalid count of threads. Enter count of threads");

                for (int i = 0; i < threadCount; i++)
                    loginInfo.Enqueue(new LoginInfo(logins[i].Split(';')[0], logins[i].Split(';')[1]));

                int successful = 0;
                int failed = 0;
                CountdownEvent countdownEvent = new CountdownEvent(loginInfo.Count);

                foreach (LoginInfo login in loginInfo)
                {
                    var thread = new Thread(() =>
                    {
                        LoginClient loginClient = new LoginClient();
                        if (loginClient.LogIn(login.Login, login.Password) == null)
                            Interlocked.Increment(ref successful);
                        else 
                            Interlocked.Increment(ref failed);
                        countdownEvent.Signal();
                    });
                    thread.Start();
                }

                countdownEvent.Wait();

                var serialization = new Serialization();
                serialization.Serialize(successful, failed);
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is IndexOutOfRangeException)
            {
                Console.WriteLine(" Sourse file not found or corrupted.");
            }      
        }
    }
}
