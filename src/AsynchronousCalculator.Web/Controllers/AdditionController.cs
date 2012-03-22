using System;
using System.Web.Mvc;
using AsynchronousCalculator.Core;

namespace AsynchronousCalculator.Web.Controllers
{
	public class AdditionController : Controller
	{
		private readonly Lazy<Calculator> _calculator = new Lazy<Calculator>(() => new Calculator());

		public ActionResult Index()
		{
			return View(model: _calculator.Value.GetLastResult());
		}

		public ActionResult Create(int x, int y)
		{
			_calculator.Value.EnqueueAddition(x, y);

			return RedirectToAction("Index");
		}
	}
}
