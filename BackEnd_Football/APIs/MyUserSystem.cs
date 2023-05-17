using BackEnd_Football.Models;
using Microsoft.EntityFrameworkCore;
using static BackEnd_Football.APIs.MyOrder;
using System.Globalization;
using static BackEnd_Football.APIs.MyStadium;

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

        public async Task<bool> confirmOrderSysAsync(string token, string m_order)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }

                SqlState? state = context.sqlStates!.Where(s => s.code == 1 && s.isdeleted == false).FirstOrDefault();
                if (state == null)
                {
                    return false;
                }

                SqlOrderStadium? order = context.sqlOrderStadium!
                    .Include(s => s.stateOrder)
                    .Where(s => s.isFinish == false && s.isDelete == false && s.code.CompareTo(m_order) == 0 && s.stateOrder!.code == 4).FirstOrDefault();

               if(order == null)
                {
                    return false;
                }

                order.stateOrder = state;
              
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

        public async Task<bool> cancelOrderSysAsync(string token, string m_order)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }

                SqlState? state = context.sqlStates!.Where(s => s.code == 6 && s.isdeleted == false).FirstOrDefault();
                if (state == null)
                {
                    return false;
                }

                SqlOrderStadium? order = context.sqlOrderStadium!
                    .Include(s => s.stateOrder)
                    .Where(s => s.isFinish == false && s.isDelete == false && s.code.CompareTo(m_order) == 0 && s.stateOrder!.code == 4).FirstOrDefault();

                if (order == null)
                {
                    return false;
                }

                order.stateOrder = state;

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

        public List<order> getListOrderConfirm(string token)
        {
            using (DataContext context = new DataContext())
            {
                List<order> items = new List<order>();
                SqlUserSystem? user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return new List<order>();
                }
                List<SqlOrderStadium> orders = context.sqlOrderStadium!.Include(s => s.stateOrder)
                                                                        .Where(s => s.isDelete == false && s.stateOrder!.code == 4)
                                                                        .Include(s => s.stadiumOrder)
                                                                        .Include(s => s.userOrder).ToList();
                foreach (SqlOrderStadium tmp in orders)
                {
                    order item = new order();
                    item.date = tmp.startTime.ToLocalTime().ToString("dd/MM/yyyy");
                    item.time = tmp.startTime.ToLocalTime().ToString("HH:mm");
                    item.nameStadium = tmp.stadiumOrder!.name;
                    item.user = tmp.userOrder!.Name;
                    item.code = tmp.code;
                    items.Add(item);
                }
                return items;
            }
        }

        public List<order> getListAllOrderForAdmin(string token, DateTime time)
        {
            using (DataContext context = new DataContext())
            {

                //DateTime start_time = DateTime.ParseExact(time, "yyyy/MM/dd HH:mm:ss", null);
                List<order> items = new List<order>();
                SqlUserSystem? m_user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (m_user == null)
                {
                    return new List<order>();
                }

                Console.WriteLine(time);

                List<SqlOrderStadium> orders = context.sqlOrderStadium!
                                                      .Include(s => s.stateOrder)
                                                      .Where(s => s.isDelete == false && s.stateOrder!.code == 1 && DateTime.Compare(s.startTime.Date, time.Date) == 0)
                                                      .Include(s => s.stadiumOrder).ToList();
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




        public ItemStadium getInfoStadiumForAdmin(string token, string name)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return new ItemStadium();
                }

                SqlStadium? emp = context.sqlStadium!.Where(s => s.isDelete == false && s.name.CompareTo(name) == 0).FirstOrDefault();
                if (emp == null)
                {
                    return new ItemStadium();
                }
                ItemStadium item = new ItemStadium();
                item.name = emp.name;
                item.address = emp.address;
                item.contact = emp.contact;
                item.price = emp.price;
                if (emp.images != null)
                {
                    item.images.AddRange(emp.images);
                }

                return item;
            }
        }

        public class itemUser
        {
            public string name { get; set; } = "";
            public string phone { get; set; } = "";
            public string email { get; set; } = "";
            public string birthday { get; set; } = "";
            public string username { get; set; } = "";
        }
        public List<itemUser> listUserForAdmin(string token)
        {
            using (DataContext context = new DataContext())
            {

                SqlUserSystem? admin = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (admin == null)
                {
                    return new List<itemUser>();
                }
                List<SqlUser>? m_user = context.users!
                                                       .Where(s => s.IsDeleted == false)
                                                       .ToList();
                if (m_user == null)
                {
                    return new List<itemUser>();
                }


                List<itemUser> myUser = new List<itemUser>();

                foreach (SqlUser tmp in m_user)
                {
                    itemUser temp = new itemUser();
                    temp.name = tmp.Name;
                    temp.phone = tmp.Phone;
                    temp.email = tmp.Email;
                    temp.birthday = tmp.birthday.ToLocalTime().ToString("dd/MM/yyyy");
                    temp.username = tmp.username;

                    myUser.Add(temp);
                }
                return myUser;
            }
        }

        public async Task<bool> removeUser(string token, string username)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.username.CompareTo(username) == 0)
                                                .FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                SqlUserSystem? admin = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();

                if (admin == null)
                {
                    return false;
                }

                user.IsDeleted = true;

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

        public async Task<bool> removeTeam(string token, string team)
        {
            using (DataContext context = new DataContext())
            {
                SqlTeam? m_team = context.SqlTeams!.Where(s => s.isdeleted == false && s.name.CompareTo(team) == 0)
                                                .FirstOrDefault();
                if (m_team == null)
                {
                    return false;
                }
                SqlUserSystem? admin = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();

                if (admin == null)
                {
                    return false;
                }

                m_team.isdeleted = true;

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
