using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BackEnd_Football.APIs.MyOrder;

namespace BackEnd_Football.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        public class orderStadium
        {

        }
        [HttpPost]
        [Route("createOrder")]
        public async Task<IActionResult> CreateOrderAsync([FromHeader] string token, M_order m_Order)
        {
            string order = await Program.api_orderStadium.createOrderAsync(token, m_Order);
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
        [Route("updateOrder")]
        public async Task<IActionResult> UpdateOrderAsync([FromHeader] string token,string code, M_order m_Order)
        {
            string order = await Program.api_orderStadium.updateOrderAsync(token,code, m_Order);
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
        [Route("deleteOrder")]
        public async Task<IActionResult> DeleteOrderAsync([FromHeader] string token, string code)
        {
            bool order = await Program.api_orderStadium.deleteOrderAsync(token, code);
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
        [Route("getInfoOrder")]
        public IActionResult getInfoOrder([FromHeader] string token, string code)
        {
            return Ok(Program.api_orderStadium!.GetInfoOrderForCustomer(token,code));
        }

    }
}
