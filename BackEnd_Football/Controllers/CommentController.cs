using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Football.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public class Comments
        {

        }

        [HttpPost]
        [Route("createComments")]
        public async Task<IActionResult> createNewsAsync([FromHeader] string token, string news, string comment)
        {

            bool flag = await Program.api_commment.createCommentAsync(token, news, comment);
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
        [Route("editComments")]
        public async Task<IActionResult> editCommentAsync([FromHeader] string token,string news, string comment)
        {

            bool flag = await Program.api_commment.editCommentAsync(token, news, comment);
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
        [Route("deleteComments")]
        public async Task<IActionResult> deleteCommentAsync([FromHeader] string token)
        {

            bool flag = await Program.api_commment.deletedCommentAsync(token);
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
        [Route("listCommentsInNews")]
        public IActionResult listCommentsInNews([FromHeader] string token, string news)
        {
            return Ok(Program.api_commment.getListNewsInNews(token, news));
        }
    }
}
