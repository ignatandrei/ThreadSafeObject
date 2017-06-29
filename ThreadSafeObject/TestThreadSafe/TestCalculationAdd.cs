using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Threading.Tasks;
using ThreadSafeObject;
using TestObject;

namespace TestThreadSafe
{
    [TestClass]
    public class TestCalculationAdd
    {
        [TestMethod]
        public void NotThreadSafeAdd()
        {
            int nrIterations = 100000;
            Calculation c = new Calculation();
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < nrIterations; i++)
            {
                var t = new Task<int>(() => c.Add());
                tasks.Add(t);
            }
            tasks.ForEach(t => t.Start());
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Result not thread safe" + c.i);
            Assert.AreNotEqual(nrIterations, c.i);
        }
        [TestMethod]
        public void ThreadSafeAdd()
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
            Assert.AreEqual(nrIterations, c.i);
        }
    }
}
