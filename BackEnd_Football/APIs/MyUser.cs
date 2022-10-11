using BackEnd_Football.Models;
using Microsoft.EntityFrameworkCore;
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
                    user.birthday = DateTime.Parse("10/02/2001");
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
                    user.birthday = DateTime.Parse("10/02/2001");
                    context.users!.Add(user);
                    int rows = await context.SaveChangesAsync();
                }
            }
        }
    }
}
