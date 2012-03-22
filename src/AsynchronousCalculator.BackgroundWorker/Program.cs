using AsynchronousCalculator.Core;
using System;
using System.Linq;
using System.Threading;

namespace AsynchronousCalculator.BackgroundWorker
{
	public class Program
	{
		private static Calculator _calculator = new Calculator();

		static void Main(string[] args)
		{
			while (true)
			{
				var value = _calculator.DequeueOperation();
				if (value == null)
				{
					Thread.Sleep(10 * 1000);
					continue;
				}

				Console.WriteLine("Request received: " + value);

				Handle(value);
			}
		}

		private static void Handle(string value)
		{
			var result = "Unknown operation (" + value + ")";

			var operation = value.Split(new[] { ' ' }, count: 2);
			var numbers = operation.Last().Split(' ').Select(x => int.Parse(x));

			switch (operation.First())
			{
				case "+":
					result = numbers.Sum().ToString();
					break;
			}

			Console.WriteLine("Response sent: " + result);

			_calculator.EnqueueResult(result);
		}
	}
}
