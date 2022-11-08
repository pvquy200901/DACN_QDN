﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BackEnd_Football.APIs.MyUser;
using static BackEnd_Football.APIs.MyNews;
using static BackEnd_Football.APIs.MyOrder;

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
        public async Task<IActionResult> registerUserAsync(ItemHttpInfoUser user)
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


            bool flag = await Program.api_user.editAsync(token, team.name, team.shortName, team.quantity, team.address, team.phone, team.des);
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
        [Route("revomeUserInTeam")]
        public async Task<IActionResult> joinTeamAsync([FromHeader] string token, string team, string name)
        {


            bool flag = await Program.api_user.removeUserInTeam(token, team,name);
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
        [Route("joinTeam")]
        public async Task<IActionResult> joinTeamAsync([FromHeader] string token, string team)
        {


            bool flag = await Program.api_user.joinTeamAsync(token, team);
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
        [Route("outTeam")]
        public async Task<IActionResult> outTeamAsync([FromHeader] string token, string team)
        {


            bool flag = await Program.api_user.outTeamAsync(token, team);
            if (flag)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("getInfoTeam")]
        public IActionResult getInfoTeam([FromHeader] string token, string team)
        {
            return Ok(Program.api_myTeam.getInfoTeam(token, team));
        }

        [HttpGet]
        [Route("listTeam")]
        public IActionResult listEmployee()
        {
            return Ok(Program.api_myTeam.getList());
        }

        [HttpGet]
        [Route("listUserSystemForCustomer")]
        public IActionResult listUserSystem([FromHeader] string token)
        {
            return Ok(Program.api_userSystem.getListUserSystemForCustomer(token));
        }

        [HttpGet]
        [Route("getListUserInTeam")]
        public IActionResult listUserInTeam([FromHeader] string token)
        {
            return Ok(Program.api_user.listUserInTeam(token));
        }

        [HttpGet]
        [Route("listStadium")]
        public IActionResult listStadium([FromHeader] string token)
        {

            return Ok(Program.api_myStadium.getList());
        }


        [HttpPut]
        [Route("setLogoTeam")]
        public async Task<IActionResult> setLogoTeamAsync([FromHeader] string token, string team, IFormFile image)
        {
           
                using (MemoryStream ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    string code = await Program.api_myTeam.setLogoAsync(token, team, ms.ToArray());
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

        [HttpDelete]
        [Route("clearLogoTeam")]
        public async Task<IActionResult> clearLogoTeamAsync([FromHeader] string token, string team)
        {
           
                bool flag = await Program.api_myTeam.clearLogoAsync(token, team);
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
        [Route("addImageTeam")]
        public async Task<IActionResult> addImageTeamAsync([FromHeader] string token, string team, IFormFile image)
        {
           
                using (MemoryStream ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    string code = await Program.api_myTeam.addImageTeamAsync(token, team, ms.ToArray());
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

        [HttpDelete]
        [Route("removeImageTeam")]
        public async Task<IActionResult> removeImageTeamAsync([FromHeader] string token, string team, string code)
        {
           
                bool flag = await Program.api_myTeam.removeImageTeamAsync(token, team, code);
                if (flag)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
           
        }

        [HttpGet]
        [Route("listOrder")]
        public IActionResult listOrder([FromHeader] string token)
        {

            return Ok(Program.api_orderStadium.getListOrder(token));
        }
    }
}
