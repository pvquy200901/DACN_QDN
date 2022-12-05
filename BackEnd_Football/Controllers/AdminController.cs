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

       

        //[HttpDelete]
        //[Route("clearLogoTeam")]
        //public async Task<IActionResult> clearLogoTeamAsync([FromHeader] string token, string team)
        //{
        //    long id = Program.api_userSystem.checkAdmin(token);
        //    if (id >= 0)
        //    {
        //        bool flag = await Program.api_myTeam.clearLogoAsync(team);
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
        //[Route("addImageTeam")]
        //public async Task<IActionResult> addImageTeamAsync([FromHeader] string token, string team, IFormFile image)
        //{
        //    long id = Program.api_userSystem.checkAdmin(token);
        //    if (id >= 0)
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            image.CopyTo(ms);
        //            string code = await Program.api_myTeam.addImageTeamAsync(team, ms.ToArray());
        //            if (string.IsNullOrEmpty(code))
        //            {
        //                return BadRequest();
        //            }
        //            else
        //            {
        //                return Ok(code);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}

        //[HttpDelete]
        //[Route("removeImageTeam")]
        //public async Task<IActionResult> removeImageTeamAsync([FromHeader] string token, string team, string code)
        //{
        //    long id = Program.api_userSystem.checkAdmin(token);
        //    if (id >= 0)
        //    {
        //        bool flag = await Program.api_myTeam.removeImageTeamAsync(team, code);
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
        [Route("listTeam")]
        public IActionResult listTeam([FromHeader] string token)
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
        //==================Quản lí Sân============================================
        public class ItemHttpStadium
        {
            public string name { get; set; } = "";
            public string address { get; set; } = "";
            public string contact { get; set; } = "";
            public int price { get; set; }
        }
        public class M_ItemStadium
        {
            public string address { get; set; } = "";
            public string contact { get; set; } = "";
            public int price { get; set; }
        }

        [HttpPost]
        [Route("createStadium")]
        public async Task<IActionResult> createStadiumAsync([FromHeader] string token, ItemHttpStadium stadium)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                bool flag = await Program.api_myStadium.createAsync(token, stadium.name, stadium.address, stadium.contact, stadium.price);
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

        [HttpPost]
        [Route("editStadium")]
        public async Task<IActionResult> editStadiumAsync([FromHeader] string token,string name, M_ItemStadium stadium)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                bool flag = await Program.api_myStadium.editAsync(token,name, stadium);
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

        [HttpDelete]
        [Route("deleteStadium")]
        public async Task<IActionResult> deleteStadiumAsync([FromHeader] string token, string stadium)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                bool flag = await Program.api_myStadium.deleteAsync(stadium);
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

        //[HttpGet]
        //[Route("getInfoStadium")]
        //public IActionResult getInfoStadium([FromHeader] string token, string stadium)
        //{
        //    long id = Program.api_userSystem.checkUserSystem(token);
        //    if (id >= 0)
        //    {
        //        return Ok(Program.api_myStadium.getInfoTeam(token, stadium));
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}

        [HttpPut]
        [Route("sendEmail")]
        public async Task<IActionResult> sendEmailAsync(string email)
        {
            
                    string code = await Program.api_gmail.sendEmailNotification(email);
                    if (string.IsNullOrEmpty(code))
                    {
                        return BadRequest();
                    }
                    else
                    {
                        return Ok(code);
                    }
        }

        [HttpPut]
        [Route("addImageStadium")]
        public async Task<IActionResult> addImageStadiumAsync([FromHeader] string token, string stadium, IFormFile image)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    string code = await Program.api_myStadium.addImageStadiumAsync(stadium, ms.ToArray());
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
        [Route("removeImageStadium")]
        public async Task<IActionResult> removeImageStadiumAsync([FromHeader] string token, string stadium, string code)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                bool flag = await Program.api_myStadium.removeImageStadiumAsync(stadium, code);
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
        [Route("listStadium")]
        public IActionResult listStadium([FromHeader] string token)
        {
            long id = Program.api_userSystem.checkUserSystem(token);
            if (id >= 0)
            {
                return Ok(Program.api_myStadium.getList());
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("listUser")]
        public IActionResult listUser([FromHeader] string token)
        {
            long id = Program.api_userSystem.checkUserSystem(token);
            if (id >= 0)
            {
                return Ok(Program.api_userSystem.listUserForAdmin(token));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete]
        [Route("revomeUser")]
        public async Task<IActionResult> deleteUserAsync([FromHeader] string token,  string username)
        {


            bool flag = await Program.api_userSystem.removeUser(token, username);
            if (flag)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("revomeTeam")]
        public async Task<IActionResult> deleteTeamAsync([FromHeader] string token, string team)
        {


            bool flag = await Program.api_userSystem.removeTeam(token, team);
            if (flag)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("revomeNews")]
        public async Task<IActionResult> deleteNewsAsync([FromHeader] string token, string code)
        {
            bool flag = await Program.api_myNews.deleteNewsForAdminAsync(token, code);
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
        [Route("denyNews")]
        public async Task<IActionResult> denyNewsAsync([FromHeader] string token, string code)
        {
            bool flag = await Program.api_myNews.denyNewsForAdminAsync(token, code);
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
        [Route("getTotalPriceInDay")]
        public IActionResult listTotalInDay([FromHeader] string token)
        {
            long id = Program.api_userSystem.checkUserSystem(token);
            if (id >= 0)
            {
                return Ok(Program.api_orderStadium.getTotalPriceToday(token));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("getTotalPriceInMonth")]
        public IActionResult listTotalInMonth([FromHeader] string token)
        {
            long id = Program.api_userSystem.checkUserSystem(token);
            if (id >= 0)
            {
                return Ok(Program.api_orderStadium.getTotalPriceMonth(token));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("getTotalOrderInMonth")]
        public IActionResult listTotalOrderInMonth([FromHeader] string token)
        {
            long id = Program.api_userSystem.checkUserSystem(token);
            if (id >= 0)
            {
                return Ok(Program.api_orderStadium.getTotalOrderMonth(token));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("listAllOrderForAdmin")]
        public IActionResult listAllOrderForAdmin([FromHeader] string token, string time)
        {
            DateTime m_time = DateTime.MinValue;
            try
            {
                m_time = DateTime.ParseExact(time, "MM/dd/yyyy", null);
            }
            catch (Exception e)
            {
                m_time = DateTime.MaxValue;
            }

            return Ok(Program.api_userSystem.getListAllOrderForAdmin(token, m_time));
        }

        [HttpGet]
        [Route("getInfoStadiumForAdmin")]
        public IActionResult getInfoStadiumForAdmin([FromHeader] string token, string name)
        {
            return Ok(Program.api_userSystem.getInfoStadiumForAdmin(token, name));
        }
    }
}
