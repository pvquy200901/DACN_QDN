using BackEnd_Football.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Football.APIs
{
    public class MyGroupChat
    {
        public async Task<bool> createChatAsync(string token, string team, string chat)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Include(s => s.SqlTeam).Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0 && s.SqlTeam!.name.CompareTo(team) == 0).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }

                SqlTeam? M_team = context.SqlTeams!.Where(s => s.isdeleted == false && s.name.CompareTo(team) == 0).FirstOrDefault();
                if (M_team == null)
                {
                    return false;
                }

                GroupChat tmp = new GroupChat();
                tmp.id = DateTime.Now.Ticks;
                tmp.time = DateTime.Now;
                tmp.chat = chat;
                tmp.team = M_team;
                tmp.useName = user;

                context.sqlGroupChat!.Add(tmp);
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
        public async Task<bool> deletedChatAsync(string token)
        {
            using (DataContext context = new DataContext())
            {
                SqlUser? user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                GroupChat? m_chat = context.sqlGroupChat!.Include(s => s.useName).Where(s => s.isDelete == false && s.useName!.token.CompareTo(token) == 0).FirstOrDefault();

                if (m_chat == null)
                {
                    return false;
                }

                m_chat.isDelete = true;


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
        public class ItemChat
        {
            public string time { get; set; } = "";
            public string chat { get; set; } = "";
            public string userComment { get; set; } = "";
        }

        public List<ItemChat> getListChatNotMine(string token, string team)
        {
            using (DataContext context = new DataContext())
            {
                List<ItemChat> l_items = new List<ItemChat>();

                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();

                List<GroupChat> listChats = context.sqlGroupChat!.Include(s => s.useName).Include(s => s.team).Where(s => s.isDelete == false && s.team!.name.CompareTo(team) == 0 && s.useName!.token.CompareTo(token) != 0)
                                                              .OrderByDescending(s => s.time).ToList();
                foreach (GroupChat chats in listChats)
                {
                    ItemChat itemChats = new ItemChat();
                    itemChats.time = chats.time.ToUniversalTime().ToString();
                    itemChats.chat = chats.chat;
                    itemChats.userComment = chats.useName!.Name;

                    l_items.Add(itemChats);
                }
                return l_items;
            }
        }

        public List<ItemChat> getListMyChat(string token, string team)
        {
            using (DataContext context = new DataContext())
            {
                List<ItemChat> l_items = new List<ItemChat>();

                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();

                List<GroupChat> listChats = context.sqlGroupChat!.Include(s => s.useName).Include(s => s.team).Where(s => s.isDelete == false && s.team!.name.CompareTo(team) == 0 && s.useName!.token.CompareTo(token) == 0)
                                                              .OrderByDescending(s => s.time).ToList();
                foreach (GroupChat chats in listChats)
                {
                    ItemChat itemChats = new ItemChat();
                    itemChats.time = chats.time.ToUniversalTime().ToString();
                    itemChats.chat = chats.chat;
                    itemChats.userComment = chats.useName!.Name;

                    l_items.Add(itemChats);
                }
                return l_items;
            }
        }

        public List<ItemChat> getListChatInTeam(string token, string team)
        {
            using (DataContext context = new DataContext())
            {
                List<ItemChat> l_items = new List<ItemChat>();

                SqlUser? m_user = context.users!.Where(s => s.IsDeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (m_user == null)
                {
                    return new List<ItemChat>();
                }

                List<GroupChat> listChats = context.sqlGroupChat!.Include(s => s.useName).Include(s => s.team).Where(s => s.isDelete == false && s.team!.name.CompareTo(team) == 0)
                                                              .OrderByDescending(s => s.time).ToList();
                foreach (GroupChat chats in listChats)
                {
                    ItemChat itemChats = new ItemChat();
                    itemChats.time = chats.time.ToUniversalTime().ToString();
                    itemChats.chat = chats.chat;
                    itemChats.userComment = chats.useName!.Name;

                    l_items.Add(itemChats);
                }
                return l_items;
            }
        }

       
    }
}
