using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Football.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
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
            return Ok(Program.api_userSystem.login(user.username, user.password));
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

        //Quản lí team

        //[HttpPost]
        //[Route("createTeam")]
        //public async Task<IActionResult> createTeamAsync([FromHeader] string token, ItemHttpTeam team)
        //{
        //    long id = Program.api_userSystem.checkAdmin(token);
        //    if (id >= 0)
        //    {
        //        bool flag = await Program.api_myTeam.createAsync(team.name, team.shortName, team.quantity, team.address ,team.phone, team.des);
        //        if (flag)
        //        {
        //            return Ok();
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}

        //[HttpPut]
        //[Route("editTeam")]
        //public async Task<IActionResult> editTeamAsync([FromHeader] string token, ItemHttpTeam team)
        //{
        //    long id = Program.api_userSystem.checkAdmin(token);
        //    if (id >= 0)
        //    {
        //        bool flag = await Program.api_myTeam.createAsync(team.name, team.shortName, team.quantity, team.address, team.phone, team.des);
        //        if (flag)
        //        {
        //            return Ok();
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}

        //[HttpDelete]
        //[Route("deleteTeam")]
        //public async Task<IActionResult> deleteTeamAsync([FromHeader] string token, ItemHttpTeam team)
        //{
        //    long id = Program.api_userSystem.checkAdmin(token);
        //    if (id >= 0)
        //    {
        //        bool flag = await Program.api_myTeam.createAsync(team.name, team.shortName, team.quantity, team.address, team.phone, team.des);
        //        if (flag)
        //        {
        //            return Ok();
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}

        [HttpGet]
        [Route("getInfoTeam")]
        public IActionResult getInfoTeam([FromHeader] string token, string team)
        {
            long id = Program.api_userSystem.checkUserSystem(token);
            if (id >= 0)
            {
                return Ok(Program.api_myTeam.getInfoTeam(token, team));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        [Route("setLogoTeam")]
        public async Task<IActionResult> setLogoTeamAsync([FromHeader] string token, string team, IFormFile image)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    string code = await Program.api_myTeam.setLogoAsync(team, ms.ToArray());
                    if (string.IsNullOrEmpty(code))
                    {
                        return BadRequest();
                    }
                    else
                    {
                        return Ok(code);
                    }
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete]
        [Route("clearLogoTeam")]
        public async Task<IActionResult> clearLogoTeamAsync([FromHeader] string token, string team)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                bool flag = await Program.api_myTeam.clearLogoAsync(team);
                if (flag)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        [Route("addImageTeam")]
        public async Task<IActionResult> addImageTeamAsync([FromHeader] string token, string team, IFormFile image)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    string code = await Program.api_myTeam.addImageTeamAsync(team, ms.ToArray());
                    if (string.IsNullOrEmpty(code))
                    {
                        return BadRequest();
                    }
                    else
                    {
                        return Ok(code);
                    }
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete]
        [Route("removeImageEmployee")]
        public async Task<IActionResult> removeImageTeamAsync([FromHeader] string token, string team, string code)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                bool flag = await Program.api_myTeam.removeImageTeamAsync(team, code);
                if (flag)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("listTeam")]
        public IActionResult listEmployee([FromHeader] string token)
        {
            long id = Program.api_userSystem.checkUserSystem(token);
            if (id >= 0)
            {
                return Ok(Program.api_myTeam.getList());
            }
            else
            {
                return Unauthorized();
            }
        }
        //=============================================================================================================================================================================

      
    }
}
