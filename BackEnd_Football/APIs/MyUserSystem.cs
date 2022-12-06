using BackEnd_Football.Models;
using Microsoft.EntityFrameworkCore;
using static BackEnd_Football.APIs.MyOrder;
using System.Globalization;

namespace BackEnd_Football.APIs
{
    public class MyUserSystem
    {
        public MyUserSystem()
        { }

        public async Task initAsync()
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? user = context.sqlUserSystems!.Where(s => s.user.CompareTo("admin") == 0).FirstOrDefault();
                if (user == null)
                {
                    SqlUserSystem item = new SqlUserSystem();
                    item.ID = DateTime.Now.Ticks;
                    item.user = "admin";
                    item.username = "admin";
                    item.password = "123456";
                    item.role = context.sqlRoles!.Where(s => s.isdeleted == false && s.code.CompareTo("admin") == 0).FirstOrDefault();
                    item.token = "1234567890";
                    item.des = "admin";
                    item.phoneNumber = "123456";
                    item.isdeleted = false;
                    context.sqlUserSystems!.Add(item);
                }


                int rows = await context.SaveChangesAsync();
            }
        }
        public class InfoUserSystem
        {
            public string user { get; set; } = "";
            public string token { get; set; } = "";
            public string role { get; set; } = "";
        }

        public InfoUserSystem login(string username, string password)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.username.CompareTo(username) == 0 && s.password.CompareTo(password) == 0).Include(s => s.role).AsNoTracking().FirstOrDefault();
                if (user == null)
                {
                    return new InfoUserSystem();
                }
                InfoUserSystem info = new InfoUserSystem();
                info.user = user.user;
                info.token = user.token;
                info.role = user.role!.code;
                return info;
            }
        }

        private string createToken()
        {
            using (DataContext context = new DataContext())
            {
                string token = DataContext.randomString(64);
                while (true)
                {
                    SqlUserSystem? user = context.sqlUserSystems!.Where(s => s.token.CompareTo(token) == 0).AsNoTracking().FirstOrDefault();
                    if (user == null)
                    {
                        break;
                    }
                    token = DataContext.randomString(64);
                }
                return token;
            }
        }

        public async Task<bool> createUserAsync(string token, string i_user, string username, string password, string phone, string display, string des, string role)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(i_user) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(role))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? own = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).Include(s => s.role).FirstOrDefault();
                if (own == null)
                {
                    return false;
                }
                if (own.role == null)
                {
                    return false;
                }
                if (own.role.code.CompareTo("admin") != 0)
                {
                    return false;
                }

                SqlUserSystem? user = context.sqlUserSystems!.Where(s => s.isdeleted == false && (s.user.CompareTo(i_user) == 0 || s.username.CompareTo(username) == 0)).FirstOrDefault();
                if (user != null)
                {
                    return false;
                }

                SqlRole? m_role = context.sqlRoles!.Where(s => s.isdeleted == false && s.name.CompareTo(role) == 0).FirstOrDefault();
                if (role == null)
                {
                    return false;
                }

                user = new SqlUserSystem();
                user.ID = DateTime.Now.Ticks;
                user.user = i_user;
                user.username = username;
                user.password = password;
                user.phoneNumber = phone;
                user.des = des;
                user.role = m_role;
                user.isdeleted = false;
                user.token = createToken();

                context.sqlUserSystems!.Add(user);

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

        public async Task<bool> editUserAsync(string token, string i_user, string password, string phone, string des, string role)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(i_user))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.user.CompareTo(i_user) == 0).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                if (user.token.CompareTo(token) != 0)
                {
                    SqlUserSystem? tmp = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).Include(s => s.role).FirstOrDefault();
                    if (tmp == null)
                    {
                        return false;
                    }
                    if (tmp.role?.code.CompareTo("admin") != 0)
                    {
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(password))
                {
                    user.password = password;
                    user.token = createToken();
                }

                if (!string.IsNullOrEmpty(phone))
                {
                    user.phoneNumber = phone;
                }
                if (!string.IsNullOrEmpty(des))
                {
                    user.des = des;
                }
                if (!string.IsNullOrEmpty(role))
                {
                    SqlRole? m_role = context.sqlRoles!.Where(s => s.isdeleted == false && s.name.CompareTo(role) == 0).FirstOrDefault();
                    if (role == null)
                    {
                        return false;
                    }
                    user.role = m_role;
                }

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

        public async Task<bool> deletedUserAsync(string token, string i_user)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.user.CompareTo(i_user) == 0).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                if (user.token.CompareTo(token) != 0)
                {
                    SqlUserSystem? tmp = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).Include(s => s.role).FirstOrDefault();
                    if (tmp == null)
                    {
                        return false;
                    }
                    if (tmp.role!.code.CompareTo("admin") != 0)
                    {
                        return false;
                    }
                }

                user.isdeleted = true;

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
        public async Task<string> setAvatarAsync(string user, byte[] file)
        {
            if (string.IsNullOrEmpty(user))
            {
                return "";
            }
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? m_user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.user.CompareTo(user) == 0).FirstOrDefault();
                if (m_user == null)
                {
                    return "";
                }

                string code = await Program.api_myFile.saveFileAsync(string.Format("{0}.jpg", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")), file);
                if (string.IsNullOrEmpty(code))
                {
                    return "";
                }

                m_user.avatar = code;

                int rows = await context.SaveChangesAsync();
                if (rows > 0)
                {
                    return code;
                }
                else
                {
                    return "";
                }
            }
        }

        public async Task<bool> clearAvatarAsync(string user)
        {
            if (string.IsNullOrEmpty(user))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? m_user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.user.CompareTo(user) == 0).FirstOrDefault();
                if (m_user == null)
                {
                    return false;
                }

                m_user.avatar = "";

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

        public class ItemUserSystem
        {
            public string user { get; set; } = "";
            public string username { get; set; } = "";
            public string displayName { get; set; } = "";
            public string des { get; set; } = "";
            public string phoneNumber { get; set; } = "";
            public string avatar { get; set; } = "";
            public string role { get; set; } = "";
        }
        public class infoUserSystem
        {
            public string displayName { get; set; } = "";
            public string phone { get; set; } = "";
            public string role { get; set; } = "";
            public string avatar { get; set; } = "";
        }
        public List<infoUserSystem> getListUserSystem(string token)
        {
            using (DataContext context = new DataContext())
            {
                List<infoUserSystem> items = new List<infoUserSystem>();
                SqlUserSystem? m_user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).Include(s => s.role).FirstOrDefault();
                if (m_user == null)
                {
                    return items;
                }
                if (m_user.role == null)
                {
                    return items;
                }
                if (m_user.role!.code.CompareTo("admin") == 0)
                {
                    List<SqlUserSystem> users = context.sqlUserSystems!.Where(s => s.isdeleted == false).Include(s => s.role).ToList();
                    foreach (SqlUserSystem user in users)
                    {
                        infoUserSystem item = new infoUserSystem();

                        item.avatar = user.avatar;
                        item.phone = user.phoneNumber;

                        if (user.role != null)
                        {
                            item.role = user.role.name;
                        }
                        items.Add(item);
                    }
                }

                return items;
            }
        }

        public List<infoUserSystem> getListUserSystemForCustomer(string token)
        {
            using (DataContext context = new DataContext())
            {
                List<infoUserSystem> items = new List<infoUserSystem>();
                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (m_user == null)
                {
                    return items;
                }

                List<SqlUserSystem> users = context.sqlUserSystems!.Where(s => s.isdeleted == false).Include(s => s.role).ToList();
                foreach (SqlUserSystem user in users)
                {
                    infoUserSystem item = new infoUserSystem();

                    item.displayName = user.user;
                    item.avatar = user.avatar;
                    item.phone = user.phoneNumber;

                    if (user.role != null)
                    {
                        item.role = user.role.name;
                    }
                    items.Add(item);
                }


                return items;
            }
        }
        public long checkAdmin(string token)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).Include(s => s.role).FirstOrDefault();
                if (user == null)
                {
                    return -1;
                }
                if (user.role == null)
                {
                    return -1;
                }
                if (user.role.code.CompareTo("admin") == 0 || user.role.code.CompareTo("manager") == 0)
                {
                    return user.ID;
                }
                else
                {
                    return -1;
                }
            }
        }

        public long checkUserSystem(string token)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).Include(s => s.role).FirstOrDefault();
                if (user == null)
                {
                    return -1;
                }
                else
                {
                    return user.ID;
                }
            }
        }

        public async Task<string> createOrderSysAsync(string token, M_order m_order)
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
              /*  SqlUserSystem? manager = context.sqlUserSystems!.Where(s => s.isdeleted == false).FirstOrDefault();
                if (manager == null)
                {
                    return "";
                }*/
                SqlOrderStadium order = new SqlOrderStadium();
                order.id = DateTime.Now.Ticks;
              
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
                order.userManagerOrder = user;

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

    }
}
