using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PrimesSearchWithThreads
{
    public class Program
    {
        static ThreadSafeList<int> FindPrimes(int primesFrom, int primesTo, ThreadSafeList<int> primes)
        {
            if (primesFrom > primesTo)
            {
                primesFrom = 1;
                primesTo = 1;
            }
                
            IEnumerable<int> sequence = Enumerable.Range(primesFrom, primesTo - primesFrom + 1).
                Where(x => x > 1 && Enumerable.Range(2, x - 2).All(y => x % y != 0));

            foreach (int number in sequence)
            {
                primes.Add(number);
            }

            return primes;
        }

        static void Main(string[] args)
        {
            bool success = true;
            string error = null;
            ThreadSafeList<int> primes = new ThreadSafeList<int>();
            List<Settings> settings = new List<Settings>();
            Stopwatch stopwatch = new Stopwatch();
            List<int> resultPrimes = new List<int>();
            
            try
            {
                settings = JsonConvert.DeserializeObject<List<Settings>>(File.ReadAllText("settings.json"));
                CountdownEvent countdownEvent = new CountdownEvent(settings.Count);
                stopwatch.Start();

                foreach (Settings setting in settings)
                {
                    var thread = new Thread(() =>
                    {
                        FindPrimes(setting.PrimesFrom, setting.PrimesTo, primes);
                        countdownEvent.Signal();
                    }); 
                    thread.Start();
                }

                countdownEvent.Wait();
                stopwatch.Stop();
                resultPrimes = primes.ToList();
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is JsonException)
            {
                success = false;
                error = "settings.json are missing or corrupted";
                resultPrimes = null;
            }

            var result = new Result(success, error, stopwatch.Elapsed.ToString(), resultPrimes);
            var json = JsonConvert.SerializeObject(result);
            var jobject = JObject.Parse(json);
            File.WriteAllText("result.json", jobject.ToString());
        }
    }
}
