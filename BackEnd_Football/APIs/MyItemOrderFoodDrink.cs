using BackEnd_Football.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static BackEnd_Football.APIs.MyOrder;

namespace BackEnd_Football.APIs
{
    public class MyItemOrderFoodDrink
    {
        public async Task<string> addItemOrderFDAsync(string token,  string codeOrder, long idFD, int amount)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? manager = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (manager == null)
                {
                    return "";
                }
                SqlOrderFD? order = context.sqlOrderFDs!.Where(s => s.code.CompareTo(codeOrder) == 0 && s.isDelete == false ).FirstOrDefault();
                if (order == null)
                {
                    return "";
                }
                SqlFoodDrink? items = context.sqlFoodDrinks!.Where(s => s.Id.CompareTo(idFD) == 0 && s.isDelete == false && s.amount > 0).FirstOrDefault();
                if (items == null)
                {
                    return "";
                }


                ItemOrderFoodDrink orderFD = new ItemOrderFoodDrink();
                orderFD.id = DateTime.Now.Ticks;
                orderFD.idFD = items.Id;
                orderFD.priceFD = items.sellPrice;
                orderFD.codeOrder = order.code;
                orderFD.amount = amount;
                orderFD.nameFD =items.name;
                order.price = order.price + (orderFD.priceFD*orderFD.amount);

                context.itemFoodDrinks!.Add(orderFD);
                int rows = await context.SaveChangesAsync();
                if (rows > 0)
                {
                    return orderFD.id.ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        public async Task<string> editItemOrderFDAsync(string token, long itemId, int amount)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? manager = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (manager == null)
                {
                    return "";
                }
                ItemOrderFoodDrink orderFD = context.itemFoodDrinks!.Where(s => s.id.CompareTo(itemId) == 0 && s.isDelete == false).FirstOrDefault();
                if (orderFD == null)
                {
                    return "";
                }
                SqlFoodDrink? items = context.sqlFoodDrinks!.Where(s => s.Id.CompareTo(orderFD.idFD) == 0 && s.isDelete == false && s.amount > 0).FirstOrDefault();
                if (items == null)
                {
                    return "";
                }
                SqlOrderFD? order = context.sqlOrderFDs!.Where(s => s.code.CompareTo(orderFD.codeOrder) == 0 && s.isDelete == false).FirstOrDefault();
                if (order == null)
                {
                    return "";
                }
                float sum = orderFD.amount;
                orderFD.idFD = items.Id;
                orderFD.priceFD = items.sellPrice;
                
                sum = amount - sum;
                orderFD.nameFD = items.name;
                if(sum >= 0)
                {
                    order.price = order.price + (orderFD.priceFD * sum);
                }
                else
                {
                    order.price = order.price - (orderFD.priceFD * (sum - amount));
                }
                orderFD.amount = amount;
                context.itemFoodDrinks!.Update(orderFD);
                int rows = await context.SaveChangesAsync();
                if (rows > 0)
                {
                    return "Success";
                }
                else
                {
                    return "";
                }
            }
        }

        public async Task<bool> deleteItemOrderFDAsync(string token, long itemId)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? system = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (system == null)
                {
                    return false;
                }

                ItemOrderFoodDrink orderFD = context.itemFoodDrinks!.Where(s => s.id.CompareTo(itemId) == 0).FirstOrDefault();
                if (orderFD == null)
                {
                    return false;
                }

                SqlOrderFD? order = context.sqlOrderFDs!.Where(s => s.code.CompareTo(orderFD.codeOrder) == 0 && s.isDelete ==false).FirstOrDefault();
                if (order == null)
                {
                    return false;
                }

                orderFD.isDelete = true;
                order.price = order.price - (orderFD.priceFD * orderFD.amount);
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

        public class ItemFDOrder
        {
            public string id { get; set; } = "";
            public string name { get; set; } = "";
            public long amount { get; set; } = 0;
            public float sellPrice { get; set; } = 0;

        }

        public List<ItemFDOrder> getListItemOrderFD(string token, string codeOrder)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? system = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (system == null)
                {
                    return null;
                }

                SqlOrderFD? order = context.sqlOrderFDs!.Where(s => s.code.CompareTo(codeOrder) == 0 && s.isDelete==false).FirstOrDefault();
                if (order == null)
                {
                    return null;
                }

                List<ItemFDOrder> l_items = new List<ItemFDOrder>();
                List<ItemOrderFoodDrink> listItemFD = context.itemFoodDrinks!.Where(s => s.isDelete == false && s.codeOrder.CompareTo(codeOrder)==0).ToList();
                foreach (ItemOrderFoodDrink item in listItemFD)
                {
                    ItemFDOrder itemFD = new ItemFDOrder();
                    itemFD.id = item.id.ToString();
                    itemFD.name = item.nameFD;
                    itemFD.amount = item.amount;
                    itemFD.sellPrice = item.priceFD;
                    l_items.Add(itemFD);
                }
                return l_items;
            }
        }


    }
}
