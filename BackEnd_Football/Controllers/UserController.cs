using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Football.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;

        public UserController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }
        public class ItemUserLogin
        {
            public string username { get; set; } = "";
            public string password { get; set; } = "";
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(ItemUserLogin user)
        {
            return Ok(Program.api_user.login(user.username, user.password));
        }

        public class ItemHttpInfoUser
        {
            public string name { get; set; } = "";
            public string username { get; set; } = "";
            public string password { get; set; } = "";
            public string email { get; set; } = "";
        }
        [HttpPost]
        [Route("registerUser")]
        public async Task<IActionResult> registerUserAsync( ItemHttpInfoUser user)
        {
           
                bool flag = await Program.api_user.registerUserAsync(user.name, user.username, user.password, user.email);
                if (flag)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
        }
    }
}
