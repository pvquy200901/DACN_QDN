using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BackEnd_Football.APIs.MyNews;
using static BackEnd_Football.Controllers.NewsController;
using static System.Net.Mime.MediaTypeNames;

namespace BackEnd_Football.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        public class news {

        }

        [HttpPost]
        [Route("createNews")]
        public async Task<IActionResult> createNewsAsync([FromHeader] string token, string title, string des, string shortDes, IFormFile image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.CopyTo(ms);
                string code = await Program.api_myNews.createNewsAsync(token, title, des, shortDes, ms.ToArray());
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

        [HttpPost]
        [Route("addImageNews")]
        public async Task<IActionResult> addImageNews([FromHeader] string token, string news, IFormFile image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.CopyTo(ms);
                string code = await Program.api_myNews.addImageNewsAsync(token, news, ms.ToArray());
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
        [HttpPut]
        [Route("editNews")]
        public async Task<IActionResult> UpdateOrderAsync([FromHeader] string token, string code, M_news m_News)
        {
            string news = await Program.api_myNews.editNewsAsync(token, code, m_News);
            if (string.IsNullOrEmpty(news))
            {
                return BadRequest();
            }
            else
            {
                return Ok(news);
            }
        }   
        
        [HttpPut]
        [Route("confirmNews")]
        public async Task<IActionResult> ConfirmOrderAsync([FromHeader] string token, string code)
        {
            long id = Program.api_userSystem.checkAdmin(token);
            if (id >= 0)
            {
                bool news = await Program.api_myNews.confirmNewsAsync(token, code);
                if (news == false)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(news);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete]
        [Route("deleteNews")]
        public async Task<IActionResult> DeleteNewsAsync([FromHeader] string token, string code)
        {
            bool news = await Program.api_myNews.deleteNewsAsync(token, code);
            if (news)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public class ItemHttpNews
        {
            public string title { get; set; }
        }


        // Lay thong tin News cần phê duyệt 
        [HttpGet]
        [Route("getInfo_ConfirmNews")]
        public IActionResult getInfoNews([FromHeader] string token, string news)
        {
            long id = Program.api_userSystem.checkUserSystem(token);
            if (id >= 0)
            {
                return Ok(Program.api_myNews.getInfo_ConfirmNews(token, news));
            }
            else
            {
                return Unauthorized();
            }
        }

        //Danh sach News cần phê duyệt 
        [HttpGet]
        [Route("list_ConfirmNews")]
        public IActionResult list_ConfirmNews([FromHeader] string token)
        {
            long id = Program.api_userSystem.checkUserSystem(token);
            if (id >= 0)
            {
                return Ok(Program.api_myNews.getList_ConfirmNews());
            }
            else
            {
                return Unauthorized();
            }
        } 
        
        //Danh sach News
        [HttpGet]
        [Route("list_News")]
        public IActionResult listNews()
        {
            return Ok(Program.api_myNews.getList_ConfirmedNews());
        }
        [HttpGet]
        [Route("listOKNewsForAdmin")]
        public IActionResult listOKNews()
        {
            return Ok(Program.api_myNews.getListOKNewsForAdmin());
        }
        [HttpGet]
        [Route("listDenyNews")]
        public IActionResult listDenyNews()
        {
            return Ok(Program.api_myNews.getList_DeniedNews());
        }


        //Images
        [HttpPut]
        [Route("addImageNews")]
        public async Task<IActionResult> addImageNewsAsync([FromHeader] string news, IFormFile image)
        {
            
            using (MemoryStream ms = new MemoryStream())
            {
                image.CopyTo(ms);
                string code = await Program.api_myNews.addImageNewsAsync(news, ms.ToArray());
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
        [Route("removeImageNews")]
        public async Task<IActionResult> removeImageNewsAsync([FromHeader] string link, string code)
        {
           bool flag = await Program.api_myNews.removeImageNewsAsync(link, code);
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
        [Route("getInfoNewsForCustomer")]
        public IActionResult getInfoNewsForCustomer([FromHeader] string token, string news)
        {           
                return Ok(Program.api_myNews.getInfoNewsForCustomer(token, news));   
        }
    }
}
