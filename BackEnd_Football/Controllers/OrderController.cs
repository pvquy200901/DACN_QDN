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

        [HttpPost]
        [Route("createOrderForAdmin")]
        public async Task<IActionResult> CreateOrderForAdminAsync([FromHeader] string token, M_order m_Order)
        {
            string order = await Program.api_orderStadium.createOrderForAdminAsync(token, m_Order);
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

        [HttpDelete]
        [Route("cancelOrderOfCustomer")]
        public async Task<IActionResult> cancelOrderOfCustomerAsync([FromHeader] string token, string order)
        {
            bool flag = await Program.api_orderStadium.cancelOrderOfCustomerAsync(token, order);
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
        [Route("listAllOrder")]
        public IActionResult listAllOrder([FromHeader] string token, string time)
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

            return Ok(Program.api_orderStadium.getListAllOrder(token, m_time));
        }

        [HttpGet]
        [Route("listOrderToday")]
        public IActionResult listOrderToday([FromHeader] string token)
        {


            return Ok(Program.api_orderStadium.getListOrderToDay(token));
        }

        [HttpGet]
        [Route("listOrderFinishedToday")]
        public IActionResult listOrderFinishedToday([FromHeader] string token)
        {


            return Ok(Program.api_orderStadium.getListOrderFinishedInDay(token));
        }

        [HttpGet]
        [Route("getListInfoOrderForUser")]
        public IActionResult getListInfoOrderForUser([FromHeader] string token)
        {


            return Ok(Program.api_orderStadium.getListInfoOrderForUser(token));
        }

        [HttpGet]
        [Route("listAllOrderUser")]
        public IActionResult listAllOrderUser([FromHeader] string token, string time, string stadium)
        {
            DateTime m_time = DateTime.MinValue;
            try
            {
                m_time = DateTime.ParseExact(time, "dd/MM/yyyy", null);
            }
            catch (Exception e)
            {
                m_time = DateTime.MaxValue;
            }

            return Ok(Program.api_orderStadium.getListAllOrderUser(token, m_time, stadium));
        }
    }
}
