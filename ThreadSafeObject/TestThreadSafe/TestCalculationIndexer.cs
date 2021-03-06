﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestObject;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreadSafeObject;

namespace TestThreadSafe
{
	[TestClass]
	public class TestCalculationIndexer
	{
		[TestMethod]
		public void NotThreadSafeIndexer()
		{
			int nrIterations = 100000;
			Calculation c = new Calculation();
			List<Task> tasks = new List<Task>();
			for (int i = 0; i < nrIterations; i++)
			{
				var t = new Task<int>(() => c["key"]++);
				tasks.Add(t);
			}
			tasks.ForEach(t => t.Start());
			Task.WaitAll(tasks.ToArray());
			Console.WriteLine("Result not thread safe" + c["key"]);
			Assert.AreNotEqual(nrIterations, c["key"]);
		}
		[TestMethod]
		public void ThreadSafeIndexer()
		{
			int nrIterations = 100000;
			Calculation c = new Calculation();
			dynamic ts = new ThreadSafe(c);
			List<Task> tasks = new List<Task>();
			for (int i = 0; i < nrIterations; i++)
			{
				var t = new Task<int>(() => ts["key"]++);
				tasks.Add(t);
			}
			tasks.ForEach(t => t.Start());
			Task.WaitAll(tasks.ToArray());
			Assert.AreNotEqual(nrIterations, c["key"]);
		}
	}
}