using BackEnd_Football.Models;

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
    }
}
