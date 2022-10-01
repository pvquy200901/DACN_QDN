using BackEnd_Football.Models;

namespace BackEnd_Football.APIs
{
    public class MyRole
    {
        public MyRole()
        {
        }

        public async Task initAsync()
        {
            using (DataContext context = new DataContext())
            {
                SqlRole? role = context.sqlRoles!.Where(s => s.isdeleted == false && s.code.CompareTo("admin") == 0).FirstOrDefault();
                if (role == null)
                {
                    SqlRole tmp = new SqlRole();
                    tmp.ID = DateTime.Now.Ticks;
                    tmp.code = "admin";
                    tmp.name = "admin";
                    tmp.des = "admin";
                    tmp.note = "admin";
                    tmp.isdeleted = false;
                    context.sqlRoles!.Add(tmp);
                }

                role = context.sqlRoles!.Where(s => s.isdeleted == false && s.code.CompareTo("manager") == 0).FirstOrDefault();
                if (role == null)
                {
                    SqlRole tmp = new SqlRole();
                    tmp.ID = DateTime.Now.Ticks;
                    tmp.code = "manager";
                    tmp.name = "manager";
                    tmp.des = "manager";
                    tmp.note = "manager";
                    tmp.isdeleted = false;
                    context.sqlRoles!.Add(tmp);
                }



                int rows = await context.SaveChangesAsync();
            }
        }
    }
}
