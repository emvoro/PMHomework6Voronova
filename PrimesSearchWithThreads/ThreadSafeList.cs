using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimesSearchWithThreads
{
    public class ThreadSafeList<T>
    {
        public List<T> SafeList { get; set; }

        public ThreadSafeList()
        {
            SafeList = new List<T>();
        }

        public void Add(T x)
        {
            lock (SafeList)
            {
                if(!SafeList.Contains(x))
                    SafeList.Add(x);
            }
        }

        public List<T> ToList()
        {
            return SafeList.OrderBy(x => x).ToList();
        }
    }
}
