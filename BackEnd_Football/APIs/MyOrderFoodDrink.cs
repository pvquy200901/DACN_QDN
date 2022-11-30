using BackEnd_Football.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static BackEnd_Football.APIs.MyNews;
using static BackEnd_Football.APIs.MyOrder;
using static BackEnd_Football.Controllers.OrderController;

namespace BackEnd_Football.APIs
{
    public class MyOrderFoodDrink
    {
        public MyOrderFoodDrink()
        {

        }
        public static string generatorcode()
        {
            using (DataContext context = new DataContext())
            {
                string code = DataContext.randomString(16);
                while (true)
                {
                    SqlOrderFD? tmp = context.sqlOrderFDs!.Where(s => s.code.CompareTo(code) == 0).FirstOrDefault();
                    if (tmp == null)
                    {
                        return code;
                    }
                }
            }
        }

        public async Task<string> createOrderFDAsync(string token)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? manager = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (manager == null)
                {
                    return "";
                }
                
                
                
                SqlOrderFD orderFD = new SqlOrderFD();
                orderFD.id = DateTime.Now.Ticks;
                orderFD.code = generatorcode();
                orderFD.createOrder = DateTime.Now;
                
                orderFD.userManagerOrder = manager;
                /*if (orderStadiumID != null)
                {
                    SqlOrderStadium? orderStadium = context.sqlOrderStadium!.Where(s => s.isDelete == false && s.isFinish == false && s.code.CompareTo(orderStadiumID) == 0).FirstOrDefault();
                    orderFD.orderStadium.code = orderStadium.code;
                    orderFD.price = orderStadium.price;
                }
                else
                {
                    orderFD.orderStadium.code = null;
                    orderFD.price = 0;
                }*/


                context.sqlOrderFDs!.Add(orderFD);
                int rows = await context.SaveChangesAsync();
                if (rows > 0)
                {
                    return orderFD.code;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
