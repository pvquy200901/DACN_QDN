using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BackEnd_Football.APIs.MyUser;

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

       
        [HttpPut]
        [Route("editUser")]
        public async Task<IActionResult> editUserAsync([FromHeader] string token, editUser user)
        {

            bool flag = await Program.api_user.editUserAsync(token, user);
            if (flag)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public class ItemHttpTeam
        {
            public string name { get; set; } = "";
            public string shortName { get; set; } = "";
            public string phone { get; set; } = "";
            public string logo { get; set; } = "";
            public string des { get; set; } = "";
            public string address { get; set; } = "";
            public int quantity { get; set; }
            public List<string> imageTeam { get; set; } = new List<string>();
        }
        //Quản lý team
        [HttpPost]
        [Route("createTeam")]
        public async Task<IActionResult> createTeamAsync([FromHeader] string token, ItemHttpTeam team)
        {
           
                bool flag = await Program.api_user.createAsync(token, team.name, team.shortName, team.quantity, team.address, team.phone, team.des);
                if (flag)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
        }

        [HttpPut]
        [Route("editTeam")]
        public async Task<IActionResult> editTeamAsync([FromHeader] string token, ItemHttpTeam team)
        {
           
           
                bool flag = await Program.api_user.editAsync(token,team.name, team.shortName, team.quantity, team.address, team.phone, team.des);
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
