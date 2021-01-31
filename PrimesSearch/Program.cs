using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace PrimesSearch
{
    class Program
    {
        static bool IsPrime(int number)
        {
            if (number < 2)
                return false;
            return !Enumerable.Range(2, number).Any(x => x != number && number % x == 0);
        }

        static int CheckNumber(string message)
        {
            Console.WriteLine(message);
            int number;
            while (!int.TryParse(Console.ReadLine().Trim(), out number))
               Console.WriteLine("Invalid input. " + message);
            return (number < 2) ? 0 : number;
        }

        static int CheckInteger(string message, int limit)
        {
            Console.WriteLine(message);
            int command;
            while (!int.TryParse(Console.ReadLine().Trim(), out command)
                || command < 1 || command > limit)
                Console.WriteLine("Invalid input. " + message);
            return command;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(" Prime numbers search.\n Voronova Emilia");
            int mainMenuCommand = 0;
            while(mainMenuCommand != 2)
            {
                mainMenuCommand = CheckInteger("\n 1 - Search primes\n 2 - Exit\n\n Enter command", 2);
                if (mainMenuCommand == 1)
                {
                    int primesFrom = CheckNumber("Enter lower border");
                    int primesTo = CheckNumber("Enter upper border");
                    IEnumerable<int> sequence = primesFrom <= primesTo ? Enumerable.Range(primesFrom, primesTo - primesFrom + 1) : null;
                    List<int> primes = new List<int>();
                    Stopwatch stopwatch = new Stopwatch();
                    Console.WriteLine(" 1 - LINQ (sequental search)\n 2 - PLINQ (parallel search)");
                    int command = CheckInteger("Enter command", 2);
                    switch (command)
                    {
                        case 1:
                            stopwatch.Start();
                            primes = sequence != null ? sequence.Where(x => IsPrime(x)).ToList() : new List<int>();
                            stopwatch.Stop();
                            break;
                        case 2:
                            stopwatch.Start();
                            primes = sequence != null ? sequence.AsParallel().AsOrdered().Where(x => IsPrime(x)).ToList() : new List<int>();
                            stopwatch.Stop();
                            break;
                    }
                    Console.WriteLine("\n Search time  : " + stopwatch.Elapsed);
                    Console.WriteLine(" Primes count : " + primes.Count);
                }
                else return;
            }
        }
    }
}
