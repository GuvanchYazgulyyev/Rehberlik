using Microsoft.AspNetCore.Mvc;

namespace Rehberlik.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
