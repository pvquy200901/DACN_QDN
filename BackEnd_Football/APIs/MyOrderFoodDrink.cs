using BackEnd_Football.Controllers;
using BackEnd_Football.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static BackEnd_Football.APIs.MyFoodDrink;

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
                orderFD.updateOrder = DateTime.Now;
                orderFD.userManagerOrder = manager;
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

        public async Task<string> editOrderFDAsync(string token, string codeOrder)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? manager = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (manager == null)
                {
                    return "";
                }
                SqlOrderFD? orderFD = context.sqlOrderFDs!.Where(s => s.code.CompareTo(codeOrder) == 0 && s.isDelete == false).FirstOrDefault();
                if (orderFD == null)
                {
                    return "";
                }
                
                orderFD.updateOrder = DateTime.Now;
                orderFD.userManagerOrder = manager;
                context.sqlOrderFDs!.Update(orderFD);
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

        public async Task<bool> deleteOrderFDAsync(string token, string codeOrder)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? manager = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (manager == null)
                {
                    return false;
                }
                SqlOrderFD? orderFD = context.sqlOrderFDs!.Where(s => s.code.CompareTo(codeOrder) == 0 && s.isDelete == false).FirstOrDefault();
                if (orderFD == null)
                {
                    return false;
                }

                orderFD.isDelete = true;
                orderFD.updateOrder = DateTime.Now; 
                orderFD.userManagerOrder = manager;

                int rows = await context.SaveChangesAsync();
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public class ListOrderFD
        {
            public string codeOrder { get; set; } = "";
            public string createDate { get; set; }
            public string updateDate { get; set; }
            public string createBy { get; set; }
            public float totalPrice { get; set; } = 0;

        }

        public List<ListOrderFD> getListOrderFD(string token)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? system = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (system == null)
                {
                    return null;
                }

                SqlOrderFD? order = context.sqlOrderFDs!.Where(s => s.isDelete == false).FirstOrDefault();
                if (order == null)
                {
                    return null;
                }

                List<ListOrderFD> l_items = new List<ListOrderFD>();
                List<SqlOrderFD> listItemFD = context.sqlOrderFDs!.Where(s => s.isDelete == false ).ToList();
                foreach (SqlOrderFD item in listItemFD)
                {
                    ListOrderFD itemFD = new ListOrderFD();
                    itemFD.codeOrder = item.code.ToString();
                    itemFD.createDate = item.createOrder.ToString();
                    itemFD.updateDate = item.updateOrder.ToString();
                    itemFD.createBy = system.username.ToString();
                    itemFD.totalPrice = item.price;
                    l_items.Add(itemFD);
                }
                return l_items;
            }
        }

    }
}
