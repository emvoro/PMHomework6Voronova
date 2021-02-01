using System;
using System.Linq;

namespace PrimesSearch
{
    public class PrimesOperations
    {
        public int FindPrimesCount(int leftBorder, int rightBorder)
        {
            if (leftBorder > rightBorder)
                return 0;
            return Enumerable.Range(leftBorder, rightBorder - leftBorder + 1).
                Where(x => x > 1 && Enumerable.Range(2, x - 2).All(y => x % y != 0)).Count();
        }

        public int FindPrimesCountParallel(int leftBorder, int rightBorder)
        {
            if (leftBorder > rightBorder)
                return 0;
            return Enumerable.Range(leftBorder, rightBorder - leftBorder + 1).AsParallel().AsOrdered().
                 Where(x => x > 1 && Enumerable.Range(2, x - 2).All(y => x % y != 0)).Count();
        }
    }
}
