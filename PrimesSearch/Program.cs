using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace PrimesSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Prime numbers search.\n Voronova Emilia");
            int mainMenuCommand = 0;
            var primesOperations = new PrimesOperations();
            var numbersValidation = new NumbersValidation();

            while(mainMenuCommand != 2)
            {
                mainMenuCommand = numbersValidation.CheckInteger("\n 1 - Search primes\n 2 - Exit\n\n Enter command", 2);
                
                if (mainMenuCommand == 1)
                {
                    int primesFrom = numbersValidation.CheckNumber("Enter lower border");
                    int primesTo = numbersValidation.CheckNumber("Enter upper border");
                    int primes = 0;
                    Stopwatch stopwatch = new Stopwatch();
                    Console.WriteLine("\n 1 - LINQ (sequental search)\n 2 - PLINQ (parallel search)\n");
                    int command = numbersValidation.CheckInteger("Enter command", 2);

                    switch (command)
                    {
                        case 1:
                            stopwatch.Start();
                            primes = primesOperations.FindPrimesCount(primesFrom, primesTo);
                            stopwatch.Stop();
                            break;
                        case 2:
                            stopwatch.Start();
                            primes = primesOperations.FindPrimesCountParallel(primesFrom, primesTo);
                            stopwatch.Stop();
                            break;
                    }

                    Console.WriteLine("\n Search time  : " + stopwatch.Elapsed);
                    Console.WriteLine(" Primes count : " + primes);
                }
                else return;
            }
        }
    }
}
