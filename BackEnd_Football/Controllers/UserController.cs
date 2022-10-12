using Microsoft.AspNetCore.Mvc;

using static BackEnd_Football.APIs.MyUser;

namespace BackEnd_Football.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        public class ItemUserSystemLogin
        {
            public string username { get; set; } = "";
            public string password { get; set; } = "";
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(ItemUserSystemLogin user)
        {
            return Ok(Program.api_user.login(user.username, user.password));
        }

        public class ItemHttpUser
        {
            public string user { get; set; } = "";
            public string displayName { get; set; } = "";
            public string phone { get; set; } = "";
            public string sex { get; set; } = "";
            public string des { get; set; } = "";
        }
       

    }
}
