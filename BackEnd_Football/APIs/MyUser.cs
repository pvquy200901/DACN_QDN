﻿using BackEnd_Football.Models;
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
                        m_user.birthday = time.ToUniversalTime();
                    }
                    else
                    {
                        m_user.birthday = DateTime.MinValue.ToUniversalTime();
                    }
                }
                catch (Exception ex)
                {
                    m_user.birthday = DateTime.MinValue.ToUniversalTime();
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

        public async Task<bool> createAsync(string token, string name, string shortName, int quantity, string address, string phone, string des)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(shortName) || quantity == 0 || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(des))
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
                team = new SqlTeam();
                team.id = DateTime.Now.Ticks;
                team.name = name;
                team.shortName = shortName;
                team.PhoneNumber = phone;
                team.des = des;
                team.isdeleted = false;
                team.quantity = 1;
                team.address = address;
                team.createdTime = DateTime.Now.ToUniversalTime();
                team.userCreateTeam = m_user;
                
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

        public async Task<bool> editAsync(string token,string name, string shortName, int quantity, string address, string phone, string des)
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
                    team!.name = name;
                }
                if (!String.IsNullOrEmpty(shortName))
                {
                    team!.shortName = shortName;
                }
                if (!String.IsNullOrEmpty(des))
                {
                    team!.des = des;
                }
                if (!String.IsNullOrEmpty(name))
                {
                    team!.name = name;
                }
                
                    team!.quantity = quantity;
                
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

        public async Task<bool> removeUserInTeam(string token,string team, string name)
        {
            using(DataContext context = new DataContext())
            {
                SqlUser? captain = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0)
                                                .Include(s => s.SqlTeam!).ThenInclude(s => s.user)
                                                .FirstOrDefault();
                if(captain == null)
                {
                    return false;
                }
                SqlTeam? i_team = null;

                if(captain.ChucVu == true)
                {
                    SqlTeam? m_team = context.SqlTeams!.Where(s=> s.isdeleted == false && s.name.CompareTo(team) == 0)
                                                    .Include(s => s.userCreateTeam)
                                                  .Include(s => s.user)
                                               .FirstOrDefault();
                    if(m_team == null)
                    {
                        return false;
                    }
                    if(m_team.user == null)
                    {
                        return false;
                    }
                    
                    if(m_team.user.Count> 0)
                    {
                        SqlUser? tmpUser = m_team.user!.Where(s => s.IsDeleted == false && s.token.CompareTo(name) == 0).FirstOrDefault();

                        if(tmpUser == null)
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
            using(DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if(user == null)
                {
                    return false;
                }
                SqlTeam? L_team = context.SqlTeams!.Where(s => s.isdeleted == false && s.name.CompareTo(team) == 0).FirstOrDefault();          
            
                if(L_team == null)
                {
                    return false;
                }

                if(user.SqlTeam == null)
                {
                    user.SqlTeam = L_team;
                    L_team.quantity += 1;
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
        }

        public List<userInTeam> listUserInTeam(string token)
        {
            using(DataContext context = new DataContext())
            {
                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if(m_user == null)
                {
                    return new List<userInTeam>();
                }
               
                SqlTeam? m_team = context.SqlTeams!.Include(s => s.user)
                                                    .Where(s => s.isdeleted == false  && s.userCreateTeam!.token.CompareTo(token) == 0)
                                                    .FirstOrDefault();
                List<userInTeam> myUser = new List<userInTeam>();
                if(m_team != null)
                {
                    if (m_team.user != null)
                    {
                        foreach (SqlUser tmp in m_team.user)
                        {
                            userInTeam temp = new userInTeam();
                            temp.name = tmp.Name;
                            temp.avatar = tmp.PhotoURL;
                            temp.phone = tmp.Phone;
                            temp.email = tmp.Email;
                            temp.birthday = tmp.birthday.ToLocalTime().ToString("dd/MM/yyyy");

                            myUser.Add(temp);
                        }
                    }
                    else
                    {
                        return new List<userInTeam>();
                    }
                }
                return myUser;

            }
        }
    }
}