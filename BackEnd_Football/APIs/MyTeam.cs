﻿using BackEnd_Football.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Football.APIs
{
    public class MyTeam
    {
        public MyTeam() { }

        public async Task initAsync()
        {
            using (DataContext context = new DataContext())
            {
               /* SqlTeam? team = context.SqlTeams!.Where(s => s.name.CompareTo("TestTeam1") == 0).FirstOrDefault();
                if (team == null)
                {
                    SqlTeam item = new SqlTeam();
                    item.id = DateTime.Now.Ticks;
                    item.name = "TestTeam1";
                    item.shortName = "TT1";
                    item.logo = "";
                    item.reputation = 100;
                    item.level = "1";
                    item.createdTime = DateTime.Now.ToUniversalTime();
                    item.quantity = 0;
                    item.address = "Thu Duc, Tp. Ho Chi Minh";
                    item.imagesTeam = new List<string>();
                    item.des = "";
                    item.PhoneNumber = "0336789234";
                    item.isdeleted = false;
                    context.SqlTeams!.Add(item);
                }

                team = context.SqlTeams!.Where(s => s.name.CompareTo("TestTeam2") == 0).FirstOrDefault();
                if (team == null)
                {
                    SqlTeam item = new SqlTeam();
                    item.id = DateTime.Now.Ticks;
                    item.name = "TestTeam2";
                    item.shortName = "TT2";
                    item.logo = "";
                    item.createdTime = DateTime.Now.ToUniversalTime();
                    item.quantity = 15;
                    item.address = "Binh Thanh, Tp. Ho Chi Minh";
                    item.imagesTeam = new List<string>();
                    item.des = "";
                    item.PhoneNumber = "0336789123";
                    item.isdeleted = false;
                    context.SqlTeams!.Add(item);
                }*/

                int rows = await context.SaveChangesAsync();
            }
        }

        //public async Task<bool> createAsync(string token,string name, string shortName, int quantity , string address,string phone, string des)
        //{
        //    if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(shortName) || quantity == 0 || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(des))
        //    {
        //        return false;
        //    }
        //    using (DataContext context = new DataContext())
        //    {
        //        SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
        //        if(m_user == null)
        //        {
        //            return false;
        //        }
        //        SqlTeam? team = context.SqlTeams!.Where(s => s.isdeleted == false && s.name.CompareTo(name) == 0).FirstOrDefault();
        //        if (team != null)
        //        {
        //            return false;
        //        }
        //        team = new SqlTeam();
        //        team.id = DateTime.Now.Ticks;
        //        team.name = name;
        //        team.shortName = shortName;
        //        team.PhoneNumber = phone;
        //        team.des = des;
        //        team.isdeleted = false;
        //        team.quantity = quantity;
        //        context.SqlTeams!.Add(team);

        //        int rows = await context.SaveChangesAsync();
        //        if (rows > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        public async Task<bool> editAsync(string name, string shortName, int quantity, string address, string phone, string des)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(shortName) || quantity == 0 || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(des))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
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
                team.quantity = quantity;
                context.SqlTeams!.Add(team);

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

        public async Task<bool> deleteAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlTeam? team = context.SqlTeams!.Where(s => s.isdeleted == false && s.name.CompareTo(name) == 0).FirstOrDefault();
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

        public async Task<string> setLogoAsync(string token, string name, byte[] file)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "";
            }
            using (DataContext context = new DataContext())
            {
                SqlTeam? team = context.SqlTeams!.Include(s => s.userCreateTeam)
                                                .Where(s => s.isdeleted == false && s.name.CompareTo(name) == 0 && s.userCreateTeam!.IsDeleted == false && s.userCreateTeam.token.CompareTo(token) == 0).FirstOrDefault();
                if (team == null)
                {
                    return "";
                }

                string code = await Program.api_myFile.saveFileAsync(string.Format("{0}.jpg", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")), file);
                if (string.IsNullOrEmpty(code))
                {
                    return "";
                }

                team.logo = code;

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

        public async Task<bool> clearLogoAsync(string token, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlTeam? team = context.SqlTeams!.Include(s => s.userCreateTeam)
                                                .Where(s => s.isdeleted == false && s.name.CompareTo(name) == 0 && s.userCreateTeam!.IsDeleted == false && s.userCreateTeam.token.CompareTo(token) == 0).FirstOrDefault();
                if (team == null)
                {
                    return false;
                }

                team.logo = "";

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

        public async Task<string> addImageTeamAsync(string token, string name, byte[] file)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "";
            }
            using (DataContext context = new DataContext())
            {
                SqlTeam? team = context.SqlTeams!.Include(s => s.userCreateTeam)
                                                .Where(s => s.isdeleted == false && s.name.CompareTo(name) == 0 && s.userCreateTeam!.IsDeleted == false && s.userCreateTeam.token.CompareTo(token) == 0).FirstOrDefault();
                if (team == null)
                {
                    return "";
                }

                string code = await Program.api_myFile.saveFileAsync(string.Format("{0}.jpg", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")), file);
                if (string.IsNullOrEmpty(code))
                {
                    return "";
                }

                if (team.imagesTeam == null)
                {
                    team.imagesTeam = new List<string>();
                }
                team.imagesTeam.Add(code);

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

        public async Task<bool> removeImageTeamAsync(string token, string name, string code)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlTeam? team = context.SqlTeams!.Include(s => s.userCreateTeam)
                                                .Where(s => s.isdeleted == false && s.name.CompareTo(name) == 0 && s.userCreateTeam!.IsDeleted == false && s.userCreateTeam.token.CompareTo(token) == 0).FirstOrDefault();
                if (team == null)
                {
                    return false;
                }

                if (team.imagesTeam == null)
                {
                    return false;
                }

                bool flag = team.imagesTeam!.Remove(code);

                if (flag == false)
                {
                    return false;
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

        public async Task<string> setAvatarAsync(string token, byte[] file)
        {
           
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return "";
                }

                string code = await Program.api_myFile.saveFileAsync(string.Format("{0}.jpg", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")), file);
                if (string.IsNullOrEmpty(code))
                {
                    return "";
                }

                user.PhotoURL = code;

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

        public class ItemTeam
        {
            public string name { get; set; } = "";
            public string shortName { get; set; } = "";
            public string phone { get; set; } = "";
            public string logo { get; set; } = "";
            public string des { get; set; } = "";
            public string address { get; set; } = "";
            public string level { get; set; } = "";
            public int reputation { get; set; }
            public int quality { get; set; }
            public List<string> imageTeam { get; set; } = new List<string>();
        }

        public List<ItemTeam> getList()
        {
            using (DataContext context = new DataContext())
            {
                List<ItemTeam> items = new List<ItemTeam>();
                List<SqlTeam> teams = context.SqlTeams!.Where(s => s.isdeleted == false).ToList();
                foreach (SqlTeam team in teams)
                {
                    ItemTeam item = new ItemTeam();
                    item.name = team.name;
                    item.shortName = team.shortName;
                    item.phone = team.PhoneNumber;
                    item.logo = team.logo;
                    item.des = team.des;
                    item.quality = team.quantity;
                    item.address = team.address;
                    item.level = team.level;
                    item.reputation = team.reputation;
                    if (team.imagesTeam != null)
                    {
                        item.imageTeam.AddRange(team.imagesTeam);
                    }
                    items.Add(item);
                }
                return items;
            }
        }

        //public List<ItemTeam> getListTeamAdd()
        //{
        //    using (DataContext context = new DataContext())
        //    {
        //        List<ItemTeam> items = new List<ItemTeam>();
        //        List<SqlTeam> teams = context.SqlTeams!.Where(s => s.isdeleted == false).ToList();
        //        foreach (SqlTeam team in teams)
        //        {
        //            ItemTeam item = new ItemTeam();
        //            item.name = team.name;
        //            item.shortName = team.shortName;
        //            item.phone = team.PhoneNumber;
        //            item.logo = team.logo;
        //            item.des = team.des;
        //            if (team.imagesTeam != null)
        //            {
        //                item.imageTeam.AddRange(team.imagesTeam);
        //            }
        //            items.Add(item);
        //        }
        //        return items;
        //    }
        //}
        public ItemTeam getInfoTeam(string token, string code)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return new ItemTeam();
                }

                SqlTeam? emp = context.SqlTeams!.Where(s => s.isdeleted == false && s.name.CompareTo(code) == 0).FirstOrDefault();
                if (emp == null)
                {
                    return new ItemTeam();
                }
                ItemTeam itemTeam = new ItemTeam();
                itemTeam.name = emp.name;
                itemTeam.shortName = emp.shortName;
                itemTeam.phone = emp.PhoneNumber;
                itemTeam.des = emp.des;
                itemTeam.logo = emp.logo;
                itemTeam.address = emp.address;
                itemTeam.quality = emp.quantity;
                itemTeam.level = emp.level;
                itemTeam.reputation = emp.reputation;
                
                if (emp.imagesTeam != null)
                {
                    itemTeam.imageTeam.AddRange(emp.imagesTeam);
                }
                return itemTeam;
            }
        }

        public ItemTeam getInfoTeamForAdmin(string token, string code)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? user = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return new ItemTeam();
                }

                SqlTeam? emp = context.SqlTeams!.Where(s => s.isdeleted == false && s.name.CompareTo(code) == 0).FirstOrDefault();
                if (emp == null)
                {
                    return new ItemTeam();
                }
                ItemTeam itemTeam = new ItemTeam();
                itemTeam.name = emp.name;
                itemTeam.shortName = emp.shortName;
                itemTeam.phone = emp.PhoneNumber;
                itemTeam.des = emp.des;
                itemTeam.logo = emp.logo;
                itemTeam.address = emp.address;
                itemTeam.quality = emp.quantity;
                if (emp.imagesTeam != null)
                {
                    itemTeam.imageTeam.AddRange(emp.imagesTeam);
                }
                return itemTeam;
            }
        }
        public ItemTeam getInfoTeamOfUser(string username)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.username.CompareTo(username) == 0).Include(s => s.SqlTeam).FirstOrDefault();
                if (user == null)
                {
                    return new ItemTeam();
                }


                ItemTeam itemTeam = new ItemTeam();

                if (user.SqlTeam != null)
                {
                    itemTeam.name = user.SqlTeam.name;
                    itemTeam.shortName = user.SqlTeam.shortName;
                    itemTeam.phone = user.SqlTeam.PhoneNumber;
                    itemTeam.des = user.SqlTeam.des;
                    itemTeam.logo = user.SqlTeam.logo;
                    itemTeam.address = user.SqlTeam.address;
                    itemTeam.quality = user.SqlTeam.quantity;
                    itemTeam.level = user.SqlTeam.level;
                    itemTeam.reputation = user.SqlTeam.reputation;

                    if (user.SqlTeam.imagesTeam != null)
                    {
                        itemTeam.imageTeam.AddRange(user.SqlTeam.imagesTeam);
                    }
                }
                
                return itemTeam;
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

                m_team.reputation -= 1;

                int rows = await context.SaveChangesAsync();
                if(rows > 0)
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
