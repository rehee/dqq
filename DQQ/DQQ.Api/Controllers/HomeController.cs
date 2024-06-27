using Microsoft.AspNetCore.Mvc;

namespace DQQ.Api.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return Content("1");
		}
	}
}
