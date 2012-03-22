using System.Web.Mvc;

namespace AsynchronousCalculator.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return RedirectToAction("Index", "Addition");
		}
	}
}
