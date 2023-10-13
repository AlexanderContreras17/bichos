using Microsoft.AspNetCore.Mvc;

namespace bichos.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		
	}
}
