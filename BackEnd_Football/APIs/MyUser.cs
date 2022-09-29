using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Football.APIs
{
    public class MyUser : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
