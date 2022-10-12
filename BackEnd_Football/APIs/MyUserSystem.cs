using BackEnd_Football.Models;
using Microsoft.EntityFrameworkCore;

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

        public List<ItemUserSystem> getList(string token)
        {
            using (DataContext context = new DataContext())
            {
                List<ItemUserSystem> items = new List<ItemUserSystem>();
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
                        ItemUserSystem item = new ItemUserSystem();
                        item.user = user.user;
                        item.username = user.username;
                        item.avatar = user.avatar;
                        item.phoneNumber = user.phoneNumber;
                        item.des = user.des;
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

    }
}
