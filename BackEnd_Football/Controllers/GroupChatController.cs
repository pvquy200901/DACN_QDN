using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Football.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupChatController : ControllerBase
    {
        public class Groupchat
        {

        }

        [HttpPost]
        [Route("createInbox")]
        public async Task<IActionResult> createChatAsync([FromHeader] string token, string team, string chat)
        {

            bool flag = await Program.api_groupchat.createChatAsync(token, team, chat);
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
        [Route("deleteChat")]
        public async Task<IActionResult> deleteChatAsync([FromHeader] string token)
        {

            bool flag = await Program.api_groupchat.deletedChatAsync(token);
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
        [Route("listChatInTeam")]
        public IActionResult listChatInTeam([FromHeader] string token, string team)
        {
            return Ok(Program.api_groupchat.getListChatNotMine(token, team));
        }


        [HttpGet]
        [Route("listMyChat")]
        public IActionResult listMyChat([FromHeader] string token, string team)
        {
            return Ok(Program.api_groupchat.getListMyChat(token, team));
        }

        [HttpGet]
        [Route("listAllChatInTeam")]
        public IActionResult listAllChatInTeam([FromHeader] string token, string team)
        {
            return Ok(Program.api_groupchat.getListChatInTeam(token, team));
        }
    }
}
