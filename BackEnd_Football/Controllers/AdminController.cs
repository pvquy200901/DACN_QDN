using BackEnd_Football.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BackEnd_Football.APIs.MyFoodDrink;
using static BackEnd_Football.APIs.MyOrder;

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
                return Ok(Program.api_myTeam.getInfoTeamForAdmin(token, team));
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
        public async Task<IActionResult> editStadiumAsync([FromHeader] string token, ItemHttpStadium stadium)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                bool flag = await Program.api_myStadium.editAsync(stadium.name, stadium.address, stadium.contact, stadium.price);
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
        public async Task<IActionResult> deleteUserAsync([FromHeader] string token, string username)
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


        //==================Tạo order bới quản lí============================================
        [HttpPost]
        [Route("createOrderStadium")]
        public async Task<IActionResult> CreateOrderSysAsync([FromHeader] string token, M_order m_Order)
        {
            string order = await Program.api_userSystem.createOrderSysAsync(token, m_Order);
            if (string.IsNullOrEmpty(order))
            {
                return BadRequest();
            }
            else
            {
                return Ok(order);
            }
        }

        [HttpPut]
        [Route("confirmOrderStadium")]
        public async Task<IActionResult> ConfirmOrderStadiumAsync([FromHeader] string token, string code)
        {
            bool order = await Program.api_userSystem.confirmOrderSysAsync(token, code);
            if (order)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("cancelOrderStadium")]
        public async Task<IActionResult> CancelOrderStadiumAsync([FromHeader] string token, string code)
        {
            bool order = await Program.api_userSystem.cancelOrderSysAsync(token, code);
            if (order)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("listConfirmOrder")]
        public IActionResult listConfirmOrder([FromHeader] string token)
        {

            return Ok(Program.api_userSystem.getListOrderConfirm(token));
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

        //==================Quản lí food drink============================================
        [HttpPost]
        [Route("createFoodDrink")]
        public async Task<IActionResult> CreateItemFDAsync([FromHeader] string token, M_foodDrink m_FoodDrink)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                bool flag = await Program.api_foodDrink.createFDAsync(token, m_FoodDrink);
                if (flag)
                {
                    return Ok(m_FoodDrink);
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
        [Route("editFoodDrink")]
        public async Task<IActionResult> editItemFDAsync([FromHeader] string token, long idFD, M_foodDrink m_FoodDrink)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                bool flag = await Program.api_foodDrink.editFDAsync(token, idFD, m_FoodDrink);
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
        [Route("deleteFoodDrink")]
        public async Task<IActionResult> deleteItemFDAsync([FromHeader] string token, long idFD)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                bool flag = await Program.api_foodDrink.deleteFDAsync(token, idFD);
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
        [Route("listFoodDrink")]
        public IActionResult listFoodDrink(string token)
        {
            long id = Program.api_userSystem.checkUserSystem(token);
            if (id >= 0)
            {
                return Ok(Program.api_foodDrink.getListFoodDrink());
            }
            else
            {
                return Unauthorized();
            }
        }

        //==================Tạo order đò ăn uống===========================================

        

        [HttpPost]
        [Route("createOrderFD")]
        public async Task<IActionResult> OrderFDCreateAsync([FromHeader] string token)
        {
            string order = await Program.api_orderFD.createOrderFDAsync(token);
            if (string.IsNullOrEmpty(order))
            {
                return BadRequest();
            }
            else
            {
                return Ok(order);
            }
        }

        [HttpPost]
        [Route("editOrderFD")]
        public async Task<IActionResult> OrderFDEditAsync([FromHeader] string token, string codeOrder)
        {
            string order = await Program.api_orderFD.editOrderFDAsync(token, codeOrder);
            if (string.IsNullOrEmpty(order))
            {
                return BadRequest();
            }
            else
            {
                return Ok(order);
            }
        }

        [HttpDelete]
        [Route("deleteOrderFD")]
        public async Task<IActionResult> OrderFDDeleteAsync([FromHeader] string token, string codeOrder)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                bool flag = await Program.api_orderFD.deleteOrderFDAsync(token, codeOrder);
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
        [Route("listOrderFD")]
        public IActionResult listOrderFD(string token)
        {
            long id = Program.api_userSystem.checkUserSystem(token);
            if (id >= 0)
            {
                return Ok(Program.api_orderFD.getListOrderFD(token));
            }
            else
            {
                return Unauthorized();
            }
        }

        //==================Thêm các đồ ăn uống vào order đã tạo===========================================
        [HttpPost]
        [Route("addItemOrderFD")]
        public async Task<IActionResult> addItemToOrderAsync([FromHeader] string token, string codeOrder, long idFD, int amount)
        {
            string order = await Program.api_addItemOrderFD.addItemOrderFDAsync(token,codeOrder, idFD,amount);
            if (string.IsNullOrEmpty(order))
            {
                return BadRequest();
            }
            else
            {
                return Ok(order);
            }
        }
        
        [HttpPost]
        [Route("editItemOrderFD")]
        public async Task<IActionResult> editItemToOrderAsync([FromHeader] string token, long itemId, int amount)
        {
            string order = await Program.api_addItemOrderFD.editItemOrderFDAsync(token, itemId, amount);
            if (string.IsNullOrEmpty(order))
            {
                return BadRequest();
            }
            else
            {
                return Ok(order);
            }
        }

        [HttpDelete]
        [Route("deleteItemOrderFD")]
        public async Task<IActionResult> deleteItemToOrderAsync([FromHeader] string token, long itemId)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                bool flag = await Program.api_addItemOrderFD.deleteItemOrderFDAsync(token, itemId);
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
        [Route("listItemOrderFD")]
        public IActionResult listItemOrderFD(string token, string codeOrder)
        {
            long id = Program.api_userSystem.checkUserSystem(token);
            if (id >= 0)
            {
                return Ok(Program.api_addItemOrderFD.getListItemOrderFD(token, codeOrder));
            }
            else
            {
                return Unauthorized();
            }
        }


    }
}
