using BackEnd_Football.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Football.APIs
{
    public class MyStadium
    {
        public MyStadium() { }

        public async Task initAsync()
        {
            using (DataContext context = new DataContext())
            {
               /* SqlStadium? stadium = context.sqlStadium!.Where(s => s.name.CompareTo("Stadium 1") == 0).FirstOrDefault();
                if (stadium == null)
                {
                    SqlStadium item = new SqlStadium();
                    item.id = DateTime.Now.Ticks;
                    item.name = "Stadium 1";
                    item.address = "TT1";
                    item.contact = "";
                    item.createdTime = DateTime.Now.ToUniversalTime();
                    item.quantity = 15;
                    item.address = "Thu Duc, Tp. Ho Chi Minh";
                    item.iamg = new List<string>();
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
    }
}
