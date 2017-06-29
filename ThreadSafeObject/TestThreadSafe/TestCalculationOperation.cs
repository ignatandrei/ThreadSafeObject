using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestObject;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreadSafeObject;

namespace TestThreadSafe
{
    [TestClass]
    public class TestCalculationOperation
    {
        [TestMethod]
        public void NotThreadSafeOperation()
        {
            int nrIterations = 100000;
            Calculation c = new Calculation();
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < nrIterations; i++)
            {
                var t = new Task<int>(() => c.Operation(2));
                tasks.Add(t);
            }
            tasks.ForEach(t => t.Start());
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Result not thread safe" + c.i);
            Assert.AreNotEqual(nrIterations*2, c.i);
        }

        [TestMethod]
        public void ThreadSafeOperation()
        {
            int nrIterations = 100000;
            Calculation c = new Calculation();
            dynamic ts = new ThreadSafe(c);
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < nrIterations; i++)
            {
                var t = new Task<int>(() => ts.Operation(2));
                tasks.Add(t);
            }
            tasks.ForEach(t => t.Start());
            Task.WaitAll(tasks.ToArray());
            Assert.AreEqual(nrIterations*2, c.i);
        }
    }
}
