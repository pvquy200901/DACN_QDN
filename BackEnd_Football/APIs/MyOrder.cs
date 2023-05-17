using BackEnd_Football.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Football.APIs
{
    public class MyOrder
    {
        public MyOrder()
        {

        }
        public static string generatorcode()
        {
            using (DataContext context = new DataContext())
            {
                string code = DataContext.randomString(16);
                while (true)
                {
                    SqlOrderStadium? tmp = context.sqlOrderStadium!.Where(s => s.code.CompareTo(code) == 0).FirstOrDefault();
                    if (tmp == null)
                    {
                        return code;
                    }
                }
            }
        }

        public class M_order
        {
            public string starttime { get; set; } = "";
            public string endtime { get; set; } = "";
            public string m_stadium { get; set; } = "";


        }
        public async Task<string> createOrderAsync(string token, M_order m_order)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return "";
                }

                SqlState? state = context.sqlStates!.Where(s => s.code == 4 && s.isdeleted == false).FirstOrDefault();
                if (state == null)
                {
                    return "";
                }

                SqlStadium? stadium = context.sqlStadium!.Include(s => s.state).Where(s => s.isDelete == false && s.state!.code == 2 && s.name.CompareTo(m_order.m_stadium) == 0).FirstOrDefault();
                if (stadium == null)
                {
                    return "";
                }
                SqlUserSystem? manager = context.sqlUserSystems!.Where(s => s.isdeleted == false).FirstOrDefault();
                if (manager == null)
                {
                    return "";
                }
                SqlOrderStadium order = new SqlOrderStadium();
                order.id = DateTime.Now.Ticks;
                order.userOrder = user;
                order.code = generatorcode();

                try
                {
                    DateTime time;
                    if (DateTime.TryParse(m_order.starttime, CultureInfo.CurrentCulture, DateTimeStyles.None, out time))
                    {
                        order.startTime = time;
                    }
                    else
                    {
                        order.startTime = DateTime.MinValue.ToUniversalTime();
                    }
                }
                catch (Exception ex)
                {
                    order.startTime = DateTime.MinValue.ToUniversalTime();
                }
                try
                {
                    DateTime time;
                    if (DateTime.TryParse(m_order.endtime, CultureInfo.CurrentCulture, DateTimeStyles.None, out time))
                    {
                        order.endTime = time;
                    }
                    else
                    {
                        order.endTime = DateTime.MinValue.ToUniversalTime();
                    }
                }
                catch (Exception ex)
                {
                    order.endTime = DateTime.MinValue.ToUniversalTime();
                }
                order.orderTime = 1.5f;
                order.price = stadium.price * 1.5f;

                order.isFinish = false;
                order.isDelete = false;

                order.stateOrder = state;
                order.stadiumOrder = stadium;
                order.userManagerOrder = manager;

                //order.stateOrder = context.sqlStates!.Where(s => s.isdeleted == false && s.code == 1).FirstOrDefault();
                context.sqlOrderStadium!.Add(order);
                int rows = await context.SaveChangesAsync();
                if (rows > 0)
                {
                    return order.code;
                }
                else
                {
                    return "";
                }

            }
        }

        public async Task<string> updateOrderAsync(string token, string code, M_order m_order)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return "";
                }


                SqlStadium? stadium = context.sqlStadium!.Where(s => s.isDelete == false && s.state!.code == 2 && s.name.CompareTo(m_order.m_stadium) == 0).FirstOrDefault();
                if (stadium == null)
                {
                    return "";
                }

                SqlOrderStadium? order = context.sqlOrderStadium!.Where(s => s.isFinish == false && s.isDelete == false
                                                                       && s.code.CompareTo(code) == 0
                                                                       && s.userOrder!.ID == user.ID).FirstOrDefault();
                if (order == null)
                {
                    return "";
                }
                //order.orderTime = m_order.ordertime;
                try
                {
                    DateTime time;
                    if (DateTime.TryParse(m_order.starttime, CultureInfo.CurrentCulture, DateTimeStyles.None, out time))
                    {
                        order.startTime = time.ToUniversalTime();
                    }
                    else
                    {
                        order.startTime = DateTime.MinValue.ToUniversalTime();
                    }
                }
                catch (Exception ex)
                {
                    order.startTime = DateTime.MinValue.ToUniversalTime();
                }
                try
                {
                    DateTime time;
                    if (DateTime.TryParse(m_order.endtime, CultureInfo.CurrentCulture, DateTimeStyles.None, out time))
                    {
                        order.endTime = time.ToUniversalTime();
                    }
                    else
                    {
                        order.endTime = DateTime.MinValue.ToUniversalTime();
                    }
                }
                catch (Exception ex)
                {
                    order.endTime = DateTime.MinValue.ToUniversalTime();
                }

                //order.price = stadium.price * m_order.ordertime;


                order.stadiumOrder = stadium;


                int rows = await context.SaveChangesAsync();
                if (rows > 0)
                {
                    return order.code;
                }
                else
                {
                    return "";
                }

            }
        }

        public async Task<bool> deleteOrderAsync(string token, string code)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }

                SqlOrderStadium? order = context.sqlOrderStadium!.Where(s => s.isFinish == false && s.isDelete == false
                                                                     && s.code.CompareTo(code) == 0
                                                                     && s.userOrder!.ID == user.ID).Include(s => s.stadiumOrder).ThenInclude(s => s!.state)
                                                                     .FirstOrDefault();

                if (order == null)
                {
                    return false;
                }

                order.isDelete = true;
                order.stateOrder = context.sqlStates!.Where(s => s.isdeleted == false && s.code == 11).FirstOrDefault();
                order.stadiumOrder!.state = context.sqlStates!.Where(s => s.isdeleted == false && s.code == 2).FirstOrDefault();

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

        public class infoOrder
        {
            public string nameStadium { get; set; } = "";
            public string code { get; set; } = "";
            public float priceStadium { get; set; }
            public string date { get; set; } = "";
            public float orderTime { get; set; }
            public string startTime { get; set; } = "";
            public string endTime { get; set; } = "";
            public float price { get; set; }
            public string state { get; set; } = "";
            public string address { get; set; } = "";
        }

        public class order
        {
            public string code { get; set; } = "";
            public string user { get; set; } = "";
            public string date { get; set; } = "";
            public string time { get; set; } = "";
            public string nameStadium { get; set; } = "";
        }
        public List<order> getListOrder(string token)
        {
            using (DataContext context = new DataContext())
            {
                List<order> items = new List<order>();
                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                List<SqlOrderStadium> orders = context.sqlOrderStadium!.Include(s => s.userOrder).Include(s => s.stateOrder)
                                                                        .Where(s => s.isDelete == false && s.userOrder!.token.CompareTo(token) == 0 && s.stateOrder!.code != 11)
                                                                        .Include(s => s.stadiumOrder)
                                                                        .OrderByDescending(s => s.startTime)
                                                                        .ToList();
                foreach (SqlOrderStadium tmp in orders)
                {
                    order item = new order();
                    item.date = tmp.startTime.ToLocalTime().ToString("dd/MM/yyyy");
                    item.time = tmp.startTime.ToLocalTime().ToString("HH:mm");
                    item.nameStadium = tmp.stadiumOrder!.name;
                    item.code = tmp.code;
                    items.Add(item);
                }
                return items;
            }
        }

        public List<order> getListHistory(string token)
        {
            using (DataContext context = new DataContext())
            {
                List<order> items = new List<order>();
                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                List<SqlOrderStadium> orders = context.sqlOrderStadium!.Include(s => s.userOrder).Include(s => s.stateOrder)
                                                                        .Where(s => s.isDelete == false && s.userOrder!.token.CompareTo(token) == 0 && s.stateOrder!.code == 1)
                                                                        .Include(s => s.stadiumOrder)
                                                                        .OrderByDescending(s => s.startTime)
                                                                        .ToList();
                foreach (SqlOrderStadium tmp in orders)
                {
                    order item = new order();
                    item.date = tmp.startTime.ToLocalTime().ToString("dd/MM/yyyy");
                    item.time = tmp.startTime.ToLocalTime().ToString("HH:mm");
                    item.nameStadium = tmp.stadiumOrder!.name;
                    item.code = tmp.code;
                    items.Add(item);
                }
                return items;
            }
        }
        public List<order> getListAllOrder(string token, DateTime time)
        {
            using (DataContext context = new DataContext())
            {
                List<order> items = new List<order>();
                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (m_user == null)
                {
                    return new List<order>();
                }
                
                List<SqlOrderStadium> orders = context.sqlOrderStadium!
                                                      .Include(s => s.stateOrder)
                                                      .Where(s => s.isDelete == false  && s.stateOrder!.code == 1 && DateTime.Compare(s.startTime.Date, time.Date) == 0)
                                                      .Include(s => s.stadiumOrder).ToList();
                if(orders.Count <= 0)
                {
                    return new List<order>();
                }
                foreach (SqlOrderStadium tmp in orders)
                {
                    order item = new order();
                    item.date = tmp.startTime.ToLocalTime().ToString("dd/MM/yyyy");
                    item.time = tmp.startTime.ToLocalTime().ToString("HH:mm");
                    item.nameStadium = tmp.stadiumOrder!.name;
                    item.code = tmp.code;
                    items.Add(item);
                }
                return items;
            }
        }

        public infoOrder GetInfoOrderForCustomer(string token, string code)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (m_user == null)
                {
                    return new infoOrder();
                }

                infoOrder temp = new infoOrder();
                SqlOrderStadium? m_order = context.sqlOrderStadium!.Include(s => s.userOrder)
                                                                    .Where(s => s.isDelete == false && s.isFinish == false && s.code.CompareTo(code) == 0 && s.userOrder == m_user)
                                                                    .Include(s => s.stadiumOrder)
                                                                    .Include(s => s.stateOrder)
                                                                    .FirstOrDefault();

                if (m_order == null)
                {
                    return new infoOrder();
                }

                temp.code = m_order.code;
                temp.nameStadium = m_order.stadiumOrder!.name;
                temp.date = m_order.startTime.ToLocalTime().ToString("dd/MM/yyyy");
                temp.orderTime = 1.5f;
                temp.startTime = m_order.startTime.ToLocalTime().ToString("HH:mm");
                temp.endTime = m_order.endTime.ToLocalTime().ToString("HH:mm");
                temp.price = m_order.price;
                temp.priceStadium = m_order.stadiumOrder.price;
                temp.state = m_order.stateOrder!.name;
                temp.address = m_order.stadiumOrder.address;

                return temp;
            }
        }
        public async Task<bool> cancelOrderOfCustomerAsync(string token, string code)
        {
            int state = 11;
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                SqlState? m_state = context.sqlStates!.Where(s => s.isdeleted == false && s.code == state).FirstOrDefault();
                SqlOrderStadium? order = context.sqlOrderStadium!.Where(s => s.isDelete == false && s.isFinish == false && s.code.CompareTo(code) == 0).Include(s => s.stateOrder).FirstOrDefault();
                if (order == null)
                {
                    return false;
                }
                if (order.stateOrder!.code == state)
                {
                    return false;
                }
                order.isFinish = true;
                order.stateOrder = m_state;



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


        
        public List<order> getListOrderToDay(string token)
        {
            using (DataContext context = new DataContext())
            {

                
                List<order> items = new List<order>();
                SqlUserSystem? m_user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (m_user == null)
                {
                    return new List<order>();
                }

                Console.WriteLine(DateTime.Now.Date.ToString("MM/dd/yyyy"));

                List<SqlOrderStadium> orders = context.sqlOrderStadium!
                                                      .Include(s => s.stateOrder)
                                                      .Where(s => s.isDelete == false && s.stateOrder!.code == 1 && DateTime.Compare(s.startTime.Date, DateTime.Now.Date) == 0)
                                                      .Include(s => s.stadiumOrder).ToList();
                if (orders.Count <= 0)
                {
                    return new List<order>();
                }
                foreach (SqlOrderStadium tmp in orders)
                {
                    order item = new order();
                    item.date = tmp.startTime.ToLocalTime().ToString("dd/MM/yyyy");
                    item.time = tmp.startTime.ToLocalTime().ToString("HH:mm") +"-"+  tmp.endTime.ToLocalTime().ToString("HH:mm");
                    item.nameStadium = tmp.stadiumOrder!.name;
                    item.code = tmp.code;
                    items.Add(item);
                }
                return items;
            }
        }

        public float getTotalPriceToday(string token)
        {
            float priceToday = 0f;
            using (DataContext context = new DataContext())
            {
                
                SqlUserSystem? m_user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if(m_user == null)
                {
                    return 0f;
                }
                List<SqlOrderStadium> orders = context.sqlOrderStadium!
                                                      .Include(s => s.stateOrder)
                                                      .Where(s => s.isDelete == false && s.stateOrder!.code == 1 && DateTime.Compare(s.startTime.Date, DateTime.Now.Date) == 0)
                                                      .Include(s => s.stadiumOrder).ToList();

                foreach(SqlOrderStadium tmp in orders)
                {
                    priceToday += tmp.price;
                }

            }
            return priceToday;
        }

        public float getTotalPriceMonth(string token)
        {
            float priceToday = 0f;
            using (DataContext context = new DataContext())
            {

                SqlUserSystem? m_user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (m_user == null)
                {
                    return 0f;
                }
                List<SqlOrderStadium> orders = context.sqlOrderStadium!
                                                      .Include(s => s.stateOrder)
                                                      .Where(s => s.isDelete == false && s.stateOrder!.code == 1 && s.startTime.Month == DateTime.Now.Month)
                                                      .Include(s => s.stadiumOrder).ToList();

                foreach (SqlOrderStadium tmp in orders)
                {
                    priceToday += tmp.price;
                }

            }
            return priceToday;
        }

        public float getTotalPriceYear(string token)
        {
            float priceToday = 0f;
            using (DataContext context = new DataContext())
            {
                int test = DateTime.Now.Year;
                Console.WriteLine(test);
                SqlUserSystem? m_user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (m_user == null)
                {
                    return 0f;
                }
                List<SqlOrderStadium> orders = context.sqlOrderStadium!
                                                      .Include(s => s.stateOrder)
                                                      .Where(s => s.isDelete == false && s.stateOrder!.code == 1)
                                                      .Include(s => s.stadiumOrder).ToList();

                if(orders == null)
                {
                    return 0f;
                }
                foreach (SqlOrderStadium tmp in orders)
                {
                    if(tmp.startTime.Year == test)
                    {
                        priceToday += tmp.price;
                    }
                }

            }
            return priceToday;
        }


        public int getTotalOrderMonth(string token)
        {
            int orderMonth = 0;
            using (DataContext context = new DataContext())
            {

                SqlUserSystem? m_user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (m_user == null)
                {
                    return 0;
                }
                List<SqlOrderStadium> orders = context.sqlOrderStadium!
                                                      .Include(s => s.stateOrder)
                                                      .Where(s => s.isDelete == false && s.stateOrder!.code == 1 && s.startTime.Month == DateTime.Now.Month)
                                                      .Include(s => s.stadiumOrder).ToList();

                orderMonth = orders.Count;

            }
            return orderMonth;
        }

        //public class M_order
        //{
        //    public string starttime { get; set; } = "";
        //    public string endtime { get; set; } = "";
        //    public string m_stadium { get; set; } = "";


        //}
        public async Task<string> createOrderForAdminAsync(string token, M_order m_order)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return "";
                }

                SqlState? state = context.sqlStates!.Where(s => s.code == 1 && s.isdeleted == false).FirstOrDefault();
                if (state == null)
                {
                    return "";
                }

                SqlStadium? stadium = context.sqlStadium!.Include(s => s.state).Where(s => s.isDelete == false && s.state!.code == 2 && s.name.CompareTo(m_order.m_stadium) == 0).FirstOrDefault();
                if (stadium == null)
                {
                    return "";
                }
               
                SqlOrderStadium order = new SqlOrderStadium();
                order.id = DateTime.Now.Ticks;
                order.userManagerOrder = user;
                order.code = generatorcode();

                try
                {
                    DateTime time;
                    if (DateTime.TryParse(m_order.starttime, CultureInfo.CurrentCulture, DateTimeStyles.None, out time))
                    {
                        order.startTime = time;
                    }
                    else
                    {
                        order.startTime = DateTime.MinValue.ToUniversalTime();
                    }
                }
                catch (Exception ex)
                {
                    order.startTime = DateTime.MinValue.ToUniversalTime();
                }
                try
                {
                    DateTime time;
                    if (DateTime.TryParse(m_order.endtime, CultureInfo.CurrentCulture, DateTimeStyles.None, out time))
                    {
                        order.endTime = time;
                    }
                    else
                    {
                        order.endTime = DateTime.MinValue.ToUniversalTime();
                    }
                }
                catch (Exception ex)
                {
                    order.endTime = DateTime.MinValue.ToUniversalTime();
                }
                order.orderTime = 1.5f;
                order.price = stadium.price * 1.5f;

                order.isFinish = false;
                order.isDelete = false;

                order.stateOrder = state;
                order.stadiumOrder = stadium;

                //order.stateOrder = context.sqlStates!.Where(s => s.isdeleted == false && s.code == 1).FirstOrDefault();
                context.sqlOrderStadium!.Add(order);
                int rows = await context.SaveChangesAsync();
                if (rows > 0)
                {
                    return order.code;
                }
                else
                {
                    return "";
                }

            }

           
        }

        public List<order> getListOrderFinishedInDay(string token)
        {
            using (DataContext context = new DataContext())
            {


                List<order> items = new List<order>();
                SqlUserSystem? m_user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (m_user == null)
                {
                    return new List<order>();
                }


                List<SqlOrderStadium> orders = context.sqlOrderStadium!
                                                      .Include(s => s.stateOrder)
                                                      .Where(s => s.isDelete == false && s.isFinish == true  && DateTime.Compare(s.startTime.Date, DateTime.Now.Date) == 0)
                                                      .Include(s => s.stadiumOrder)
                                                      .OrderByDescending(s => s.startTime)
                                                      .ToList();
                if (orders.Count <= 0)
                {
                    return new List<order>();
                }
                foreach (SqlOrderStadium tmp in orders)
                {
                    order item = new order();
                    item.date = tmp.startTime.ToLocalTime().ToString("dd/MM/yyyy");
                    item.time = tmp.startTime.ToLocalTime().ToString("HH:mm") + "-" + tmp.endTime.ToLocalTime().ToString("HH:mm");
                    item.nameStadium = tmp.stadiumOrder!.name;
                    item.code = tmp.code;
                    items.Add(item);
                }
                return items;
            }
        }

        public class orderUser
        {
            public string code { get; set; } = "";
            public string date { get; set; } = "";
            public string startTime { get; set; } = "";
            public string endTime { get; set; } = "";
            public string price { get; set; } = "";
            public string state { get; set; } = "";
            public string nameStadium { get; set; } = "";
            public string address { get; set; } = "";
        }

        public List<orderUser> getListInfoOrderForUser(string token)
        {
            using(DataContext context = new DataContext())
            {
                List<orderUser> list = new List<orderUser>();
                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if(m_user == null)
                {
                    return new List<orderUser>();
                }

                List<SqlOrderStadium>? m_orders = context.sqlOrderStadium!.Include(s => s.userOrder).Where(s => s.isDelete == false && s.userOrder!.username.CompareTo(m_user.username) == 0).Include(s => s.stateOrder).Include(s => s.stadiumOrder).OrderByDescending(s => s.startTime).ToList();
                if(m_orders == null)
                {
                    return new List<orderUser>();
                }
                foreach (SqlOrderStadium tmp in m_orders)
                {
                    orderUser item = new orderUser();
                    item.code = tmp.code;
                    item.date = tmp.startTime.ToLocalTime().ToString("dd-MM-yyyy");
                    item.startTime = tmp.startTime.ToLocalTime().ToString("HH:mm");
                    item.endTime = tmp.endTime.ToLocalTime().ToString("HH:mm");
                    item.price = tmp.price.ToString();
                    item.state = tmp.stateOrder!.name;
                    item.nameStadium = tmp.stadiumOrder!.name;
                    item.address = tmp.stadiumOrder!.address;
                    list.Add(item);
                }
                return list;
            }
        }

        public List<order> getListAllOrderUser(string token, DateTime time, string name)
        {
            using (DataContext context = new DataContext())
            {

                List<order> items = new List<order>();
                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (m_user == null)
                {
                    return new List<order>();
                }

                SqlStadium? m_stadium = context.sqlStadium!.Where(s => s.isDelete == false && s.name.CompareTo(name) == 0).FirstOrDefault();
                if(m_stadium == null)
                {
                    return new List<order>();
                }

                List<SqlOrderStadium> orders = context.sqlOrderStadium!
                                                      .Include(s => s.stateOrder).Include(s => s.stadiumOrder)
                                                      .Where(s => s.isDelete == false && s.stateOrder!.code == 1 && DateTime.Compare(s.startTime.Date, time.Date) == 0 && s.stadiumOrder!.name.CompareTo(name) == 0)
                                                      .Include(s => s.userOrder)
                                                      .ToList();
                if (orders.Count <= 0)
                {
                    return new List<order>();
                }
                foreach (SqlOrderStadium tmp in orders)
                {
                    order item = new order();
                    item.date = tmp.startTime.ToLocalTime().ToString("dd/MM/yyyy");
                    item.time = tmp.startTime.ToLocalTime().ToString("HH:mm");
                    item.nameStadium = tmp.stadiumOrder!.name;
                    item.code = tmp.code;
                    items.Add(item);
                }
                return items;
            }
        }

    }
    
}
