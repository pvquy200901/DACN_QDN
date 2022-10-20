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
        public string generatorcode()
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
            public int ordertime { get; set; } = 0;
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

                SqlState? state = context.sqlStates!.Where(s => s.code == 1 && s.isdeleted == false).FirstOrDefault();
                if (state == null)
                {
                    return "";
                }

                SqlStadium? stadium = context.sqlStadium!.Where(s => s.isDelete == false && s.state!.code == 2 && s.name.CompareTo(m_order.m_stadium) == 0).Include(s => s.state).FirstOrDefault();
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
                order.orderTime = m_order.ordertime;
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

                order.price = stadium.price * m_order.ordertime;

                order.isFinish = false;
                order.isDelete = false;

                order.stateOrder = state;
                order.stadiumOrder = stadium;
                order.userManagerOrder = manager;

                stadium.state = context.sqlStates!.Where(s => s.isdeleted == false && s.code == 1).FirstOrDefault();
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

        public async Task<string> updateOrderAsync(string token,string code, M_order m_order)
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
                if(order == null)
                {
                    return "";
                }
                order.orderTime = m_order.ordertime;
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

                order.price = stadium.price * m_order.ordertime;


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
    }



}
