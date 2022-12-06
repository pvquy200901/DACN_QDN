using BackEnd_Football.Models;
using Microsoft.EntityFrameworkCore;
namespace BackEnd_Football.APIs
{
    public class MyComment
    {
        public MyComment() { }

        public async Task<bool> createCommentAsync(string token, string news, string comment)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }

                SqlNews? M_news = context.sqlNews!.Where(s => s.code.CompareTo(news) == 0).FirstOrDefault();
                if (M_news == null)
                {
                    return false;
                }

                Comment tmp = new Comment();
                tmp.id = DateTime.Now.Ticks;
                tmp.time = DateTime.Now;
                tmp.comments = comment;
                tmp.News = M_news;
                tmp.useComments = user;

                context.comments!.Add(tmp);
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

        public async Task<bool> editCommentAsync(string token, string news, string comment)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(comment))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                Comment? m_comment = context.comments!
                                                     .Include(s => s.useComments)
                                                     .Include(s => s.News)
                                                     .Where(s => s.useComments!.token.CompareTo(token) == 0 && s.isDelete == false && s.News!.code.CompareTo(news) == 0)
                                                     .FirstOrDefault();

                if(m_comment == null)
                {
                    return false;
                }
                
                if (!string.IsNullOrEmpty(comment))
                {
                    m_comment.comments = comment;
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

        public async Task<bool> deletedCommentAsync(string token)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                Comment? m_comment = context.comments!.Include(s => s.useComments).Where(s => s.isDelete == false &&  s.useComments!.token.CompareTo(token) == 0).FirstOrDefault();

                if (m_comment == null)
                {
                    return false;
                }

                m_comment.isDelete = true;
                

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

        public class ItemComment
        {
            public string time { get; set; } = "";
            public string comment { get; set; } = "";
            public string userComment { get; set; } = "";
        }
        public List<ItemComment> getListNewsInNews(string token, string news)
        {
            using (DataContext context = new DataContext())
            {
                List<ItemComment> l_items = new List<ItemComment>();

                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();

                List<Comment> listComments = context.comments!.Include(s => s.News).Where(s => s.isDelete == false && s.News!.code.CompareTo(news) == 0)
                                                              .Include(s => s.useComments).OrderByDescending(s => s.time).ToList();
                foreach (Comment comments in listComments)
                {
                    ItemComment itemComments = new ItemComment();
                    itemComments.time = comments.time.ToUniversalTime().ToString();
                    itemComments.comment = comments.comments;
                    itemComments.userComment = comments.useComments!.Name;
                   
                    l_items.Add(itemComments);
                }
                return l_items;
            }
        }

       



    }
}
