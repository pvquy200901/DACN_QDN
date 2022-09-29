using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Football.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
