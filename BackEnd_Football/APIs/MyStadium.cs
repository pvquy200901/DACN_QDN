using BackEnd_Football.Models;
using Microsoft.EntityFrameworkCore;
using static BackEnd_Football.Controllers.AdminController;

namespace BackEnd_Football.APIs
{
    public class MyStadium
    {
        public MyStadium() { }

        public async Task initAsync()
        {
            using (DataContext context = new DataContext())
            {
                SqlStadium? stadium = context.sqlStadium!.Where(s => s.name.CompareTo("Stadium 1") == 0).FirstOrDefault();
                if (stadium == null)
                {
                    SqlStadium item = new SqlStadium();
                    item.id = DateTime.Now.Ticks;
                    item.name = "Stadium 1";
                    item.address = "Thu Duc, Tp. Ho Chi Minh";
                    item.contact = "0336789234";
                    item.isDelete = false;
                    item.isFinish = false;
                    item.price = 350000;
                    item.createdTime = DateTime.Now.ToUniversalTime();
                    context.sqlStadium!.Add(item);
                }

                stadium = context.sqlStadium!.Where(s => s.name.CompareTo("Stadium 2") == 0).FirstOrDefault();
                if (stadium == null)
                {
                    SqlStadium item = new SqlStadium();
                    item.id = DateTime.Now.Ticks;
                    item.name = "Stadium 2";
                    item.address = "Thu Duc, Tp. Ho Chi Minh";
                    item.contact = "0336789234";
                    item.isDelete = false;
                    item.isFinish = false;
                    item.price = 350000;
                    item.createdTime = DateTime.Now.ToUniversalTime();
                    context.sqlStadium!.Add(item);
                }

                int rows = await context.SaveChangesAsync();
            }
        }

        public async Task<bool> createAsync(string token, string name, string address, string contact, int price)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(contact))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlStadium? stadium = context.sqlStadium!.Where(s => s.isDelete == false && s.name.CompareTo(name) == 0).Include(s => s.state).FirstOrDefault();
                SqlState? state = context.sqlStates!.Where(s => s.isdeleted == false && s.code == 2).FirstOrDefault();
                if (stadium != null)
                {
                    return false;
                }
                if(state == null)
                {
                    return false;
                }
                SqlUserSystem? manager = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if(manager == null)
                {
                    return false;
                }
                stadium = new SqlStadium();
                stadium.id = DateTime.Now.Ticks;
                stadium.name = name;
                stadium.address = address;
                stadium.contact = contact;
                stadium.price = price;
                stadium.createdTime = DateTime.Now.ToUniversalTime();
                stadium.state = state;
                stadium.userSystem = manager;
                context.sqlStadium!.Add(stadium);

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

        public async Task<bool> editAsync(string name, string address, string contact, int price)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(contact))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                //SqlUserSystem? sqlUserSystem = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                //if(sqlUserSystem == null)
                //{
                //    return false;
                //}
                SqlStadium? stadium = context.sqlStadium!.Where(s => s.isDelete == false && s.name.CompareTo(name) == 0).FirstOrDefault();
                if (stadium != null)
                {
                    stadium = new SqlStadium();
                    stadium.id = DateTime.Now.Ticks;
                    stadium.name = name;
                    stadium.address = address;
                    stadium.contact = contact;
                    stadium.price = price;
                    context.sqlStadium!.Add(stadium);
                }

                //m_stadium.address = stadium!.address;
                //m_stadium.contact = stadium.contact;
                //m_stadium.price = stadium.price;

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
                SqlStadium? team = context.sqlStadium!.Where(s => s.isDelete == false && s.name.CompareTo(name) == 0).FirstOrDefault();
                if (team == null)
                {
                    return false;
                }
                team.isDelete = true;

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

        public async Task<string> addImageStadiumAsync(string name, byte[] file)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "";
            }
            using (DataContext context = new DataContext())
            {
                SqlStadium? team = context.sqlStadium!.Where(s => s.isDelete == false && s.name.CompareTo(name) == 0).FirstOrDefault();
                if (team == null)
                {
                    return "";
                }

                string code = await Program.api_myFile.saveFileAsync(string.Format("{0}.jpg", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")), file);
                if (string.IsNullOrEmpty(code))
                {
                    return "";
                }

                if (team.images == null)
                {
                    team.images = new List<string>();
                }
                team.images.Add(code);

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

        public async Task<bool> removeImageStadiumAsync(string name, string code)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlStadium? team = context.sqlStadium!.Where(s => s.isDelete == false && s.name.CompareTo(name) == 0).FirstOrDefault();
                if (team == null)
                {
                    return false;
                }

                if (team.images == null)
                {
                    return false;
                }

                bool flag = team.images!.Remove(code);

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

        public class ItemStadium
        {
            public string name { get; set; } = "";
            public string address { get; set; } = "";
            public string contact { get; set; } = "";
            public float price { get; set; }
            public List<string> images { get; set; } = new List<string>();
        }

        public List<ItemStadium> getList()
        {
            using (DataContext context = new DataContext())
            {
                List<ItemStadium> items = new List<ItemStadium>();
                List<SqlStadium> stadiums = context.sqlStadium!.Where(s => s.isDelete == false).ToList();
                foreach (SqlStadium stadium in stadiums)
                {
                    ItemStadium item = new ItemStadium();
                    item.name = stadium.name;
                    item.address = stadium.address;
                    item.contact = stadium.contact;
                    item.price = stadium.price;
                    if (stadium.images != null)
                    {
                        item.images.AddRange(stadium.images);
                    }
                    items.Add(item);
                }
                return items;
            }
        }

        public List<ItemStadium> getListStadiumReady()
        {
            using (DataContext context = new DataContext())
            {
                int state = 1;
                List<ItemStadium> items = new List<ItemStadium>();
                List<SqlStadium> stadiums = context.sqlStadium!.Where(s => s.isDelete == false && s.state.code == state).ToList();
                foreach (SqlStadium stadium in stadiums)
                {
                    ItemStadium item = new ItemStadium();
                    item.name = stadium.name;
                    item.address = stadium.address;
                    item.contact = stadium.contact;
                    item.price = stadium.price;
                    if (stadium.images != null)
                    {
                        item.images.AddRange(stadium.images);
                    }
                    items.Add(item);
                }
                return items;
            }
        }


        public ItemStadium getInfoStadium(string token, string name)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
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
    }
 }