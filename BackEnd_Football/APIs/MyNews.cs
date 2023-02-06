using BackEnd_Football.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BackEnd_Football.APIs.MyNews;
using static BackEnd_Football.APIs.MyOrder;
using static BackEnd_Football.APIs.MyTeam;
using static System.Net.Mime.MediaTypeNames;

namespace BackEnd_Football.APIs
{
    public class MyNews
    {
        public MyNews() {}
        public string generatorcode()
        {
            using (DataContext context = new DataContext())
            {
                string code = DataContext.randomString(16);
                while (true)
                {
                    SqlNews? tmp = context.sqlNews!.Where(s => s.code.CompareTo(code) == 0).FirstOrDefault();
                    if (tmp == null)
                    {
                        return code;
                    }
                }
            }
        }

        public class M_news
        {
            public string title { get; set; } = "";
            public string description { get; set; } = "";
            public string shortDes { get; set; } = "";
        }

        public async Task<string> createNewsAsync(string token, M_news m_news) 
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
                SqlUserSystem? manager = context.sqlUserSystems!.Where(s => s.isdeleted == false).FirstOrDefault();
                if (manager == null)
                {
                    return "";
                }

                SqlNews news = new SqlNews();
                news.id = DateTime.Now.Ticks;
                news.title = m_news.title;
                news.description = m_news.description;
                news.shortDes = m_news.shortDes;
                news.code = generatorcode();
                news.createdTime = DateTime.Now;

                news.state = state;
                news.user = user;
                //string img = await Program.api_myFile.saveFileAsync(string.Format("{0}.jpg", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")), file);
                //if (string.IsNullOrEmpty(img))
                //{
                //    return "";
                //}
                //if(news.images == null)
                //{
                //    news.images = new List<string>();
                //}
                //news.images.Add(img);
                //news.manager = manager;

                //news.state = context.sqlStates!.Where(s => s.isdeleted == false && s.code == 4).FirstOrDefault();
                context.sqlNews!.Add(news);
                int rows = await context.SaveChangesAsync();
                if (rows > 0)
                {
                    return news.code;
                }
                else
                {
                    return "";
                }
            }
        }

        public async Task<string> editNewsAsync(string token, string code, M_news m_news)
        {
            int stateNews = 4;
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return "";
                }

                SqlNews? news = context.sqlNews!.Include(s => s.user)
                                                .Where(s => s.code.CompareTo(code) == 0 && s.state!.code == stateNews
                                                                    && s.user!.ID == user.ID).FirstOrDefault();
                if (news == null)
                {
                    return "";
                }


                news.title = m_news.title;
                news.description = m_news.description;
                news.shortDes = m_news.shortDes;
                news.createdTime = DateTime.Now.ToUniversalTime();

                context.sqlNews!.Add(news);
                int rows = await context.SaveChangesAsync();
                if (rows > 0)
                {
                    return news.code;
                }
                else
                {
                    return "";
                }
            }
        }



        public async Task<string> addImageNewsAsync(string token, string news, byte[] file)
        {
            if (string.IsNullOrEmpty(news))
            {
                return "";
            }
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

                SqlNews? m_news = context.sqlNews!.Include(s => s.state).Where(s => s.code.CompareTo(news) == 0 && s.state == state).FirstOrDefault();
                if(m_news == null)
                {
                    return "";
                }

                string code = await Program.api_myFile.saveFileAsync(string.Format("{0}.jpg", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")), file);
                if (string.IsNullOrEmpty(code))
                {
                    return "";
                }

                if (m_news.images == null)
                {
                    m_news.images = new List<string>();
                }
                m_news.images.Add(code);

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
        //phê duyệt được lựa chọn là đã duyệt 
        public async Task<bool> confirmNewsAsync( string token, string code)
        {
            using (DataContext context = new DataContext())
            {
                SqlNews? news = context.sqlNews!.Where(s => s.code.CompareTo(code) == 0 && s.state!.code == 4).Include(s => s.state).FirstOrDefault();
                if (news == null)
                {
                    return false;
                }
                SqlUserSystem? userSystem = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (userSystem == null)
                {
                    return false;
                }

                SqlState? state = context.sqlStates!.Where(s => s.code == 5 && s.isdeleted == false).FirstOrDefault();
                if (state == null)
                {
                    return false;
                }

                news.state = state;


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

        public async Task<bool> deleteNewsAsync(string token, string code)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }

                SqlNews? news = context.sqlNews!.Where(s => s.code.CompareTo(code) == 0
                                                                     && s.user!.ID == user.ID ).FirstOrDefault();

                if (news == null)
                {
                    return false;
                }

                context.sqlNews!.Remove(news);
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

        public async Task<bool> deleteNewsForAdminAsync(string token, string code)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? admin = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (admin == null)
                {
                    return false;
                }

                SqlNews? news = context.sqlNews!.Where(s =>s.isDelete == false && s.code.CompareTo(code) == 0)
                                                                     .FirstOrDefault();

                if (news == null)
                {
                    return false;
                }

                news.isDelete = true;
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

        public async Task<bool> denyNewsForAdminAsync(string token, string code)
        {
            using (DataContext context = new DataContext())
            {
                SqlUserSystem? admin = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (admin == null)
                {
                    return false;
                }

                SqlNews? news = context.sqlNews!.Where(s => s.isDelete == false && s.code.CompareTo(code) == 0).Include(s => s.state)
                                                                     .FirstOrDefault();

                SqlState? m_state = context.sqlStates!.Where(s => s.isdeleted == false && s.code == 6).FirstOrDefault();

                if (news == null)
                {
                    return false;
                }

                news.isDelete = true;
                news.state = m_state;
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

        //public async Task<bool> blockUserPostAsync(string userSystem, string token)
        //{
        //    using (DataContext context = new DataContext())
        //    {
        //        SqlUserSystem? admin = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(userSystem) == 0).FirstOrDefault();
        //        if (admin == null)
        //        {
        //            return false;
        //        }

        //        SqlNews? news = context.sqlNews!.Where(s => s.isDelete == false && s.code.CompareTo(code) == 0).Include(s => s.state)
        //                                                             .FirstOrDefault();

        //        SqlState? m_state = context.sqlStates!.Where(s => s.isdeleted == false && s.code == 6).FirstOrDefault();

        //        if (news == null)
        //        {
        //            return false;
        //        }

        //        news.isDelete = true;
        //        news.state = m_state;
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

        public class ItemInfoNews
        {
            public string title { get; set; } = "";
            public string description { get; set; } = "";
            public string shortDes { get; set; } = "";
            public string createdTime { get; set; } = "";
            public List<string> imagesNews{ get; set; } = new List<string>();
        }

        // Lay thong tin News cần phê duyệt - admin 
        public ItemInfoNews getInfo_ConfirmNews(string token, string code)
        {
            using (DataContext context = new DataContext())
            {
                SqlNews? news = context.sqlNews!.Where(s =>  s.code.CompareTo(code) == 0).Include(s => s.state).FirstOrDefault();
                if (news == null)
                {
                    return new ItemInfoNews();
                }

                SqlUserSystem? userSystem = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (userSystem == null)
                {
                    return new ItemInfoNews();
                }

                ItemInfoNews itemNews = new ItemInfoNews();
                itemNews.title = news.title;
                itemNews.description = news.description;
                itemNews.shortDes = news.shortDes;
                itemNews.createdTime = news.createdTime.ToString();
                if (news.images != null)
                {
                    itemNews.imagesNews.AddRange(news.images);
                }
                return itemNews;
            }
        }

        public class ItemNews
        {
            public string code { get; set; } = "";
            public string title { get; set; } = "";
            public string shortDes { get; set; } = "";
            public string createdTime { get; set; } = "";
            public string Time { get; set; } = "";

        }

        //Lấy danh sách các news cần phê duyệt  -admin
        public List<ItemNewsForAdmin> getList_ConfirmNews()
        {
            using (DataContext context = new DataContext())
            {
                List<ItemNewsForAdmin> l_items = new List<ItemNewsForAdmin>();
                List<SqlNews> listNews = context.sqlNews!.Where(s => s.isDelete == false && s.state!.code == 4).Include(s => s.user).OrderByDescending(s => s.createdTime).ToList();
                foreach (SqlNews news in listNews)
                {
                    ItemNewsForAdmin itemNews = new ItemNewsForAdmin();
                    itemNews.code = news.code;
                    itemNews.title = news.title;
                    //itemNews.description = news.description;
                    itemNews.shortDes = news.shortDes;
                    itemNews.createdTime = news.createdTime.ToString();
                    itemNews.user = news.user!.Name;
                  /*  if (news.images != null)
                    {
                        itemNews.imagesNews.AddRange(news.images);
                    }*/
                    l_items.Add(itemNews);
                }
                return l_items;
            }
        } 

        // danh sách các news
        public List<ItemNews> getList_ConfirmedNews()
        {
            using (DataContext context = new DataContext())
            {
                List<ItemNews> l_items = new List<ItemNews>();
                List<SqlNews> listNews = context.sqlNews!.Where(s => s.isDelete ==false && s.state!.code == 5).OrderByDescending(s => s.createdTime).ToList();
                foreach (SqlNews news in listNews)
                {
                    ItemNews itemNews = new ItemNews();
                    itemNews.code = news.code;
                    itemNews.title = news.title;
                    itemNews.shortDes = news.shortDes;
                    itemNews.createdTime = news.createdTime.ToString();
                    l_items.Add(itemNews);
                }
                return l_items;
            }
        }


        public class ItemNewsV2
        {
            public string code { get; set; } = "";
            public string title { get; set; } = "";
            public string shortDes { get; set; } = "";
            public string createdTime { get; set; } = "";
            public string name { get; set; } = "";
            public string email { get; set; } = "";
            public string token { get; set; } = "";

        }

        public List<ItemNewsV2> getList_DeniedNews()
        {
            using (DataContext context = new DataContext())
            {
                List<ItemNewsV2> l_items = new List<ItemNewsV2>();
                List<SqlNews> listNews = context.sqlNews!.Where(s => s.isDelete == true && s.state!.code == 6).Include(s => s.user).ToList();
                foreach (SqlNews news in listNews)
                {
                    ItemNewsV2 itemNews = new ItemNewsV2();
                    itemNews.code = news.code;
                    itemNews.title = news.title;
                    itemNews.shortDes = news.shortDes;
                    itemNews.createdTime = news.createdTime.ToString();
                    itemNews.name = news.user!.Name;
                    itemNews.token = news.user!.token;
                    itemNews.email = news.user!.Email;
                    l_items.Add(itemNews);
                }
                return l_items;
            }
        }


        public List<ItemNews> getListNewsForUser(string token)
        {
            using (DataContext context = new DataContext())
            {
                List<ItemNews> l_items = new List<ItemNews>();
                List<SqlNews> listNews = context.sqlNews!.Include(s => s.user).Where(s => s.isDelete == false && s.state!.code == 5 && s.user!.IsDeleted == false && s.user!.token.CompareTo(token) == 0).ToList();
                foreach (SqlNews news in listNews)
                {
                    ItemNews itemNews = new ItemNews();
                    itemNews.code = news.code;
                    itemNews.title = news.title;
                    itemNews.shortDes = news.shortDes;
                    itemNews.createdTime = news.createdTime.ToString("dd/MM/yyyy");
                    itemNews.Time = news.createdTime.ToString("HH:mm");
                    l_items.Add(itemNews);
                }
                return l_items;
            }
        }
            //Image

            public async Task<string> addImageNewsAsync(string code, byte[] file)
        {
            if (string.IsNullOrEmpty(code))
            {
                return "";
            }
            using (DataContext context = new DataContext())
            {
                SqlNews? news = context.sqlNews!.Where(s => s.code.CompareTo(code) == 0).FirstOrDefault();
                if (news == null)
                {
                    return "";
                }

                string link = await Program.api_myFile.saveFileAsync(string.Format("{0}.jpg", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")), file);
                if (string.IsNullOrEmpty(link))
                {
                    return "";
                }

                if (news.images == null)
                {
                    news.images = new List<string>();
                }
                news.images.Add(link);

                int rows = await context.SaveChangesAsync();
                if (rows > 0)
                {
                    return link;
                }
                else
                {
                    return "";
                }
            }
        }

        public async Task<bool> removeImageNewsAsync(string link, string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlNews? news = context.sqlNews!.Where(s => s.code.CompareTo(code) == 0).FirstOrDefault();
                if (news == null)
                {
                    return false;
                }

                if (news.images == null)
                {
                    return false;
                }

                bool flag = news.images!.Remove(link);

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

        public ItemInfoNews getInfoNewsForCustomer(string token, string code)
        {
            using (DataContext context = new DataContext())
            {
                SqlNews? news = context.sqlNews!.Include(s => s.state).Where(s => s.code.CompareTo(code) == 0 && s.state!.code == 5).FirstOrDefault();
                if (news == null)
                {
                    return new ItemInfoNews();
                }

                SqlUser? user= context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return new ItemInfoNews();
                }

                ItemInfoNews itemNews = new ItemInfoNews();
                itemNews.title = news.title;
                itemNews.description = news.description;
                itemNews.shortDes = news.shortDes;
                itemNews.createdTime = news.createdTime.ToString();
                if (news.images != null)
                {
                    itemNews.imagesNews.AddRange(news.images);
                }
                return itemNews;
            }
        }

        public class ItemNewsForAdmin
        {
            public string code { get; set; } = "";
            public string title { get; set; } = "";
            public string shortDes { get; set; } = "";
            public string createdTime { get; set; } = "";
            public string Time { get; set; } = "";
            public string user { get; set; } = "";

        }

        public List<ItemNewsForAdmin> getListOKNewsForAdmin()
        {
            using (DataContext context = new DataContext())
            {
                List<ItemNewsForAdmin> l_items = new List<ItemNewsForAdmin>();
                List<SqlNews> listNews = context.sqlNews!.Where(s => s.isDelete == false && s.state!.code == 5).Include(s => s.user).ToList();
                foreach (SqlNews news in listNews)
                {
                    ItemNewsForAdmin itemNews = new ItemNewsForAdmin();
                    itemNews.code = news.code;
                    itemNews.title = news.title;
                    itemNews.shortDes = news.shortDes;
                    itemNews.createdTime = news.createdTime.ToString();
                    itemNews.user = news.user!.Name;
                    l_items.Add(itemNews);
                }
                return l_items;
            }
        }
    }
}
