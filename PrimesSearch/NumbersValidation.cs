using System;
using System.Collections.Generic;
using System.Text;

namespace PrimesSearch
{
    public class NumbersValidation
    {
        public int CheckNumber(string message)
        {
            Console.WriteLine(message);
            int number;
            while (!int.TryParse(Console.ReadLine().Trim(), out number))
                Console.WriteLine("Invalid input. " + message);
            return (number < 2) ? 0 : number;
        }

        public int CheckInteger(string message, int limit)
        {
            Console.WriteLine(message);
            int command;
            while (!int.TryParse(Console.ReadLine().Trim(), out command)
                || command < 1 || command > limit)
                Console.WriteLine("Invalid input. " + message);
            return command;
        }
    }
}
