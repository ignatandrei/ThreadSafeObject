using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestObject;
using ThreadSafeObject;

namespace TestNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            int nrIterations = 100000;
            Calculation c = new Calculation();
            dynamic ts = new ThreadSafe(c);
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < nrIterations; i++)
            {
                var t = new Task<int>(() => ts.Add());
                tasks.Add(t);
            }
            tasks.ForEach(t => t.Start());
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"task iterations {nrIterations} result: {c.i}");
        }
    }
}