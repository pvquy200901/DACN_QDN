using BackEnd_Football.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace BackEnd_Football.APIs
{
    public class MyUser
    {
        public MyUser()
        {
        }
       

        private string createToken()
        {
            using (DataContext context = new DataContext())
            {
                string token = DataContext.randomString(64);
                while (true)
                {
                    SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).AsNoTracking().FirstOrDefault();
                    if (user == null)
                    {
                        break;
                    }
                    token = DataContext.randomString(32);
                }
                return token;
            }
        }
        public async Task initAsync()
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.UID.CompareTo("Captain") == 0).FirstOrDefault();
                if (user == null)
                {
                    user = new SqlUser();
                    user.ID = DateTime.Now.Ticks;
                    user.token = "1234567890";
                    user.UID = "Captain";
                    user.Name = "Captain";
                    user.username = "captain";
                    user.password = "123456";
                    user.Email = "";
                    user.PhotoURL = "";
                    user.Phone = "";
                    user.IsDeleted = false;
                    user.ChucVu = true;
                    //user.birthday = DateTime.Parse("10/02/2001");
                    context.users!.Add(user);
                    int rows = await context.SaveChangesAsync();
                }

                user = context.users!.Where(s => s.UID.CompareTo("User") == 0).FirstOrDefault();
                if (user == null)
                {
                    user = new SqlUser();
                    user.ID = DateTime.Now.Ticks;
                    user.token = createToken();
                    user.UID = "User";
                    user.Name = "User";
                    user.username = "user";
                    user.password = "123456";
                    user.Email = "";
                    user.PhotoURL = "";
                    user.Phone = "";
                    user.IsDeleted = false;
                    user.ChucVu = true;
                    //user.birthday = DateTime.Parse("10/02/2001");
                    context.users!.Add(user);
                    int rows = await context.SaveChangesAsync();
                }
            }
        }

        public class InfoUser
        {
            public string user { get; set; } = "";
            public string token { get; set; } = "";
            public bool ChucVu { get; set; } = true;

        }

        public InfoUser login(string username, string password)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.username.CompareTo(username) == 0 && s.password.CompareTo(password) == 0).AsNoTracking().FirstOrDefault();
                if (user == null)
                {
                    return new InfoUser();
                }
                InfoUser info = new InfoUser();
                info.user = user.Name;
                info.token = user.token;
                info.ChucVu = user.ChucVu;
                return info;
            }
        }

        public class registerUser
        {
            public string name { get; set; } = "";
            public string username { get; set; } = "";
            public string password { get; set; } = "";
            public string email { get; set; } = "";

        }

        public async Task<bool> registerUserAsync(string i_user, string username, string password, string email)
        {
            if (string.IsNullOrEmpty(i_user) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.username.CompareTo(username) == 0).FirstOrDefault();
                if (user != null)
                {
                    return false;
                }

                SqlUser teamp = new SqlUser();
                teamp.ID = DateTime.Now.Ticks;
                teamp.Name = i_user;
                teamp.username = username;
                teamp.password = password;
                teamp.Email = email;
                teamp.ChucVu = false;
                teamp.token = createToken();
                teamp.SqlState = context.sqlStates!.Where(s => s.isdeleted == false && s.code == 0).FirstOrDefault();
                context.users!.Add(teamp);

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

        public class editUser
        {
            public string name { get; set; } = "";
            public string email { get; set; } = "";
            public string phone { get; set; } = "";
            public string birthday { get; set; } = "";
        }
        public async Task<bool> editUserAsync(string token, editUser user)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (m_user == null)
                {
                    return false;
                }




                if (!string.IsNullOrEmpty(user.email))
                {
                    m_user.Email = user.email;
                }
                if (!string.IsNullOrEmpty(user.name))
                {
                    m_user.Name = user.name;
                }
                if (!string.IsNullOrEmpty(user.phone))
                {

                    m_user.Phone = user.phone;
                }

                try
                {
                    DateTime time;
                    if (DateTime.TryParse(user.birthday, CultureInfo.CurrentCulture, DateTimeStyles.None, out time))
                    {
                        m_user.birthday = time.ToLocalTime();
                    }
                    else
                    {
                        m_user.birthday = DateTime.MinValue.ToLocalTime();
                    }
                }
                catch (Exception ex)
                {
                    m_user.birthday = DateTime.MinValue.ToLocalTime();
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

        public async Task<bool> createAsync(string token, string name, string shortName,  string address, string phone, string des, string level)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(name)  || string.IsNullOrEmpty(phone))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (m_user == null)
                {
                    return false;
                }

                SqlTeam? team = context.SqlTeams!.Where(s => s.isdeleted == false && s.name.CompareTo(name) == 0).FirstOrDefault();
                if (team != null)
                {
                    return false;
                }
                SqlState? state = context.sqlStates!.Where(s => s.isdeleted == false && s.code == 1).FirstOrDefault();
                team = new SqlTeam();
                team.id = DateTime.Now.Ticks;
                team.name = name;
                team.shortName = shortName;
                team.PhoneNumber = phone;
                team.des = des;
                team.isdeleted = false;
                team.quantity = 1;
                team.reputation = 100;
                team.level = level;
                team.address = address;
                team.createdTime = DateTime.Now.ToUniversalTime();
                team.userCreateTeam = m_user;
                m_user.SqlState = state;

                context.SqlTeams!.Add(team);


                m_user.ChucVu = true;
                m_user.SqlTeam = team;
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

        public async Task<bool> editAsync(string token, string name, string shortName,  string address, string phone, string des, string level)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(name))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {

                SqlTeam? team = context.SqlTeams!.Where(s => s.isdeleted == false && s.name.CompareTo(name) == 0 && s.userCreateTeam!.token.CompareTo(token) == 0).FirstOrDefault();
                if (team == null)
                {
                    return false;
                }
                if (!String.IsNullOrEmpty(name))
                {
                    team.name = name;
                }
                if (!String.IsNullOrEmpty(shortName))
                {
                    team.shortName = shortName;
                }
                if (!String.IsNullOrEmpty(des))
                {
                    team.des = des;
                }
                if (!String.IsNullOrEmpty(name))
                {
                    team.name = name;
                }
                if (!String.IsNullOrEmpty(level))
                {
                    team.level = level;
                }

                if (!String.IsNullOrEmpty(address))
                {
                    team!.address = address;
                }
                if (!String.IsNullOrEmpty(phone))
                {
                    team!.PhoneNumber = phone;
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
        public async Task<bool> reportTeam(string token, string team)
        {
            using(DataContext context = new DataContext())
            {
                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if(m_user == null)
                {
                    return false;
                }

                SqlTeam? m_team = context.SqlTeams!.Where(s => s.isdeleted == false && s.name.CompareTo(team) == 0).FirstOrDefault();
                if(m_team == null)
                {
                    return false;
                }
                if(m_team.reputation <= 0)
                {
                    return false;
                }

                m_team.reputation = m_team.reputation - 1;

                int row = await context.SaveChangesAsync();
                if(row > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> deleteAsync(string token, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlTeam? team = context.SqlTeams!.Where(s => s.isdeleted == false && s.name.CompareTo(name) == 0 && s.userCreateTeam!.token.CompareTo(token) == 0).FirstOrDefault();
                if (team == null)
                {
                    return false;
                }
                team.isdeleted = true;

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

        public async Task<bool> removeUserInTeam(string token, string team, string name)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? captain = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0)
                                                .Include(s => s.SqlTeam!).ThenInclude(s => s.user)
                                                .FirstOrDefault();
                if (captain == null)
                {
                    return false;
                }
                SqlTeam? i_team = null;

                if (captain.ChucVu == true)
                {
                    SqlTeam? m_team = context.SqlTeams!.Include(s => s.userCreateTeam)
                                                    .Where(s => s.isdeleted == false && s.name.CompareTo(team) == 0 && s.userCreateTeam!.token.CompareTo(token) == 0)
                                                  .Include(s => s.user)
                                               .FirstOrDefault();
                    if (m_team == null)
                    {
                        return false;
                    }
                    if (m_team.user == null)
                    {
                        return false;
                    }

                    if (m_team.user.Count > 0)
                    {
                        SqlUser? tmpUser = m_team.user!.Where(s => s.IsDeleted == false && s.username.CompareTo(name) == 0).FirstOrDefault();

                        if (tmpUser == null)
                        {
                            return false;
                        }
                        m_team.user.Remove(tmpUser);
                    }
                    else
                    {
                        return false;
                    }
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
        public async Task<bool> joinTeamAsync(string token, string team)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Include(s => s.SqlState).Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                SqlTeam? L_team = context.SqlTeams!.Where(s => s.isdeleted == false && s.name.CompareTo(team) == 0).FirstOrDefault();

                if (L_team == null)
                {
                    return false;
                }

                if (user.SqlTeam == null)
                {
                    user.SqlTeam = L_team;
                    L_team.quantity += 1;
                    user.SqlState = context.sqlStates!.Where(s => s.isdeleted == false && s.code == 4).FirstOrDefault();
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

        public async Task<bool> outTeamAsync(string token, string team)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                SqlTeam? L_team = context.SqlTeams!.Where(s => s.isdeleted == false && s.name.CompareTo(team) == 0).FirstOrDefault();

                if (L_team == null)
                {
                    return false;
                }

                if (user.SqlTeam != null)
                {
                    user.SqlTeam = null;
                    L_team.quantity -= 1;
                    user.SqlState = context.sqlStates!.Where(s => s.isdeleted == false && s.code == 0).FirstOrDefault();
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

        public class userInTeam
        {
            public string name { get; set; } = "";
            public string avatar { get; set; } = "";
            public string phone { get; set; } = "";
            public string email { get; set; } = "";
            public string birthday { get; set; } = "";
            public string username { get; set; } = "";
            public bool chucVu { get; set; } = false;
        }

        public List<userInTeam> listUserInTeam(string team)
        {
            using (DataContext context = new DataContext())
            {
                List<SqlUser>? m_user = context.users!.Include(s => s.SqlTeam).Include(s => s.SqlState)
                                                       .Where(s => s.IsDeleted == false && s.SqlTeam!.name.CompareTo(team) == 0 && s.SqlState!.code == 1)
                                                       .ToList();
                if (m_user == null)
                {
                    return new List<userInTeam>();
                }


                List<userInTeam> myUser = new List<userInTeam>();

                foreach (SqlUser tmp in m_user)
                {
                    userInTeam temp = new userInTeam();
                    temp.name = tmp.Name;
                    temp.avatar = tmp.PhotoURL;
                    temp.phone = tmp.Phone;
                    temp.email = tmp.Email;
                    temp.birthday = tmp.birthday.ToLocalTime().ToString("dd/MM/yyyy");
                    temp.chucVu = tmp.ChucVu;
                    temp.username = tmp.username;

                    myUser.Add(temp);
                }
                return myUser;
            }


        }

        public List<infoUser> listUserComing(string token, string team)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? captain = context.users!.Include(s => s.SqlTeam).ThenInclude(s => s.userCreateTeam).Where(s => s.IsDeleted == false && s.SqlTeam!.userCreateTeam!.token.CompareTo(token) == 0).FirstOrDefault();
                List<SqlUser>? m_user = context.users!.Include(s => s.SqlTeam).Include(s => s.SqlState).Where(s => s.IsDeleted == false && s.SqlTeam!.name.CompareTo(team) == 0 && s.SqlState!.code == 4).ToList();
                if (m_user == null)
                {
                    return new List<infoUser>();
                }
                List<infoUser> users = new List<infoUser>();
                foreach (SqlUser tmp in m_user)
                {
                    infoUser temp = new infoUser();
                    temp.name = tmp.Name;
                    temp.avatar = tmp.PhotoURL;
                    temp.phone = tmp.Phone;
                    temp.email = tmp.Email;
                    //temp.birthday = tmp.birthday.ToLocalTime().ToString("dd/MM/yyyy");
                    temp.chucVu = tmp.ChucVu;
                    temp.username = tmp.username;
                    temp.team = tmp.SqlTeam!.name;
                    temp.state = tmp.SqlState!.code;

                    users.Add(temp);
                }
                return users;
            }
        }
        public async Task<bool> acpUser(string token, string username)
        {
            using(DataContext context = new DataContext())
            {
                SqlUser? captain = context.users!.Include(s => s.SqlTeam).ThenInclude(s => s.userCreateTeam).Where(s => s.IsDeleted == false && s.SqlTeam!.userCreateTeam!.token.CompareTo(token) == 0).FirstOrDefault();
                if (captain == null)
                {
                    return false;
                }
                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.username.CompareTo(username) == 0).Include(s => s.SqlState).FirstOrDefault();

                if(m_user == null)
                {
                    return false;
                }

                m_user.SqlState = context.sqlStates!.Where(s => s.isdeleted == false && s.code == 1).FirstOrDefault();

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

        public async Task<bool> cancelUser(string token, string username)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? captain = context.users!.Include(s => s.SqlTeam).ThenInclude(s => s.userCreateTeam).Where(s => s.IsDeleted == false && s.SqlTeam!.userCreateTeam!.token.CompareTo(token) == 0).FirstOrDefault();
                if (captain == null)
                {
                    return false;
                }
                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.username.CompareTo(username) == 0).Include(s => s.SqlState).FirstOrDefault();

                if (m_user == null)
                {
                    return false;
                }

                m_user.SqlTeam = null;
                m_user.SqlState = context.sqlStates!.Where(s => s.isdeleted == false && s.code == 0).FirstOrDefault();

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



        public class infoUser
        {
            public string name { get; set; } = "";
            public string avatar { get; set; } = "";
            public string email { get; set; } = "";
            public string phone { get; set; } = "";
            public bool chucVu { get; set; } = false;
            public string team { get; set; } = "";
            public string username { get; set; } = "";
            public string birthday { get; set; } = "";

            public int state { get; set; }
        }

        public infoUser getInfoUser(string token)
        {
            DataContext context = new DataContext();

            SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0)
                                            .Include(s => s.SqlTeam)
                                            .Include(s => s.SqlState)
                                            .FirstOrDefault();

            if (m_user == null)
            {
                return new infoUser();
            }


            infoUser temp = new infoUser();
            temp.name = m_user.Name;
            temp.avatar = m_user.PhotoURL;
            temp.email = m_user.Email;
            temp.phone = m_user.Phone;
            temp.chucVu = m_user.ChucVu;
            temp.username = m_user.username;
            temp.state = m_user.SqlState!.code;
            temp.birthday = m_user.birthday.ToString("MM/dd/yyyy");
            if (m_user.SqlTeam == null)
            {
                temp.team = "";
            }
            else
            {
                temp.team = m_user.SqlTeam!.name;
            }


            return temp;
        }

        public string getAvatarUser (string username)
        {
            DataContext context = new DataContext();

            SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.username.CompareTo(username) == 0)
                                            .Include(s => s.SqlTeam)
                                            .Include(s => s.SqlState)
                                            .FirstOrDefault();

            if (m_user == null)
            {
                return "";
            }

            string img = m_user.PhotoURL;
            return img;
        }

        public infoUser getInfoUserForAdmin(string username)
        {
            DataContext context = new DataContext();

            SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.username.CompareTo(username) == 0)
                                            .Include(s => s.SqlTeam)
                                            .Include(s => s.SqlState)
                                            .FirstOrDefault();

            if (m_user == null)
            {
                return new infoUser();
            }


            infoUser temp = new infoUser();
            temp.name = m_user.Name;
            temp.avatar = m_user.PhotoURL;
            temp.email = m_user.Email;
            temp.phone = m_user.Phone;
            temp.chucVu = m_user.ChucVu;
            temp.username = m_user.username;
            temp.state = m_user.SqlState!.code;
            if(m_user.birthday == DateTime.MinValue)
            {
                temp.birthday = "";
            }
            else
            {
                temp.birthday = m_user.birthday.ToString("dd/MM/yyyy");
            }
           
            if (m_user.SqlTeam == null)
            {
                temp.team = "";
            }
            else
            {
                temp.team = m_user.SqlTeam!.name;
            }


            return temp;
        }

        public class itemOrder
        {
            public string code { get; set; } = "";
            public string date { get; set; } = "";
            public string startTime { get; set; } = "";
            public string endTime { get; set; } = "";
            public string price { get; set; } = "";
            public string nameStadium { get; set; } = "";
            public string address { get; set; } = "";
            public string state { get; set; } = "";
        }

        public List<itemOrder> getListOrderAtTime(string token, string team)
        {
            List<itemOrder> list = new List<itemOrder>();
            DateTime time = DateTime.Now;
            DateTime begin = new DateTime(time.Year, time.Month, time.Day, 0, 0, 0);
            DateTime end = begin.AddDays(7);
            using(DataContext context = new DataContext())
            {
                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if(m_user == null)
                {
                    return new List<itemOrder>();
                }

                SqlTeam? m_team = context.SqlTeams!.Where(s => s.isdeleted == false && s.name.CompareTo(team) == 0).Include(s => s.user).FirstOrDefault();
                if(m_team == null)
                {
                    return new List<itemOrder>();
                }
               /* if(m_team.user != null)
                {
                    foreach (SqlUser item in m_team.user)
                    {
                        if(item.token.CompareTo(m_user.token) != 0)
                        {
                            return new List<itemOrder>();
                        }
                    }
                }*/

                List<SqlOrderStadium>? m_orders = context.sqlOrderStadium!.Include(s => s.stadiumOrder).Include(s => s.stateOrder).Include(s => s.userOrder!).ThenInclude(s => s.SqlTeam)
                                                .Where(s => s.isDelete == false
                                                && s.stateOrder!.code == 1
                                                && s.userOrder!.SqlTeam!.name.CompareTo(m_team.name) == 0 
                                                && (DateTime.Compare(begin, s.startTime) <= 0
                                                && DateTime.Compare(end, s.startTime) > 0)).ToList();
                if(m_orders == null)
                {
                    return new List<itemOrder>();
                }

                foreach (SqlOrderStadium item in m_orders)
                {
                    itemOrder tmp = new itemOrder();

                    tmp.code = item.code;
                    tmp.date = item.startTime.ToLocalTime().ToString("dd-MM-yyyy");
                    tmp.startTime = item.startTime.ToLocalTime().ToString("HH:mm");
                    tmp.endTime = item.endTime.ToLocalTime().ToString("HH:mm");
                    tmp.price = item.price.ToString();
                    tmp.nameStadium = item.stadiumOrder!.name;
                    tmp.address = item.stadiumOrder.address;
                    tmp.state = item.stateOrder!.name;

                    list.Add(tmp);
                }
            }

            return list;
        }
    }
}