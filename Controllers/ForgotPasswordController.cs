using Microsoft.AspNetCore.Mvc;

namespace CORE_CRUD_API.Controllers
{
	public class ForgotPasswordController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult UpdatePassword()
		{
			return View();
		}

	}
}
