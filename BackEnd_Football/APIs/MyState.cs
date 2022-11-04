using BackEnd_Football.Models;

namespace BackEnd_Football.APIs
{
    public class MyState
    {
        public MyState()
        {

        }
        public async Task initAsync()
        {
            using (DataContext context = new DataContext())
            {
                SqlState? type = context.sqlStates!.Where(s => s.code == 0).FirstOrDefault();
                if (type == null)
                {
                    SqlState item = new SqlState();
                    item.ID = DateTime.Now.Ticks;
                    item.code = 0;
                    item.name = "Mới";
                    item.des = "Mới";
                    item.isdeleted = false;
                    context.sqlStates!.Add(item);
                }
                type = context.sqlStates!.Where(s => s.code == 1).FirstOrDefault();
                if (type == null)
                {
                    SqlState item = new SqlState();
                    item.ID = DateTime.Now.Ticks;
                    item.code = 1;
                    item.name = "Xác Nhận";

                    item.des = "Xác Nhận";
                    item.isdeleted = false;
                    context.sqlStates!.Add(item);
                }

                type = context.sqlStates!.Where(s => s.code == 2).FirstOrDefault();
                if (type == null)
                {
                    SqlState item = new SqlState();
                    item.ID = DateTime.Now.Ticks;
                    item.code = 2;
                    item.name = "Sẵn sàng";
                    item.des = "Sẵn sàng";
                    item.isdeleted = false;
                    context.sqlStates!.Add(item);
                }

                type = context.sqlStates!.Where(s => s.code == 3).FirstOrDefault();
                if (type == null)
                {
                    SqlState item = new SqlState();
                    item.ID = DateTime.Now.Ticks;
                    item.code = 3;
                    item.name = "Đang thi đấu";
                    item.des = "Đang thi đấu";
                    item.isdeleted = false;
                    context.sqlStates!.Add(item);
                }  
                //=========statement---NEWS====
                type = context.sqlStates!.Where(s => s.code == 4).FirstOrDefault();
                if (type == null)
                {
                    SqlState item = new SqlState();
                    item.ID = DateTime.Now.Ticks;
                    item.code = 4;
                    item.name = "Đang chờ duyệt";
                    item.des = "Đang chờ duyệt";
                    item.isdeleted = false;
                    context.sqlStates!.Add(item);
                }
                 

                type = context.sqlStates!.Where(s => s.code == 5).FirstOrDefault();
                if (type == null)
                {
                    SqlState item = new SqlState();
                    item.ID = DateTime.Now.Ticks;
                    item.code = 5;
                    item.name = "Đã đăng";
                    item.des = "Đã đăng";
                    item.isdeleted = false;
                    context.sqlStates!.Add(item);
                }
                //===================================================

                type = context.sqlStates!.Where(s => s.code == 8).FirstOrDefault();
                if (type == null)
                {
                    SqlState item = new SqlState();
                    item.ID = DateTime.Now.Ticks;
                    item.code = 8;
                    item.name = "Kết Thúc";
                    item.des = "Kết Thúc";
                    item.isdeleted = false;
                    context.sqlStates!.Add(item);
                }

                type = context.sqlStates!.Where(s => s.code == 9).FirstOrDefault();
                if (type == null)
                {
                    SqlState item = new SqlState();
                    item.ID = DateTime.Now.Ticks;
                    item.code = 9;
                    item.name = "Xác nhận thanh toán";
                    item.des = "Xác nhận thanh toán";
                    item.isdeleted = false;
                    context.sqlStates!.Add(item);
                }
                type = context.sqlStates!.Where(s => s.code == 10).FirstOrDefault();
                if (type == null)
                {
                    SqlState item = new SqlState();
                    item.ID = DateTime.Now.Ticks;
                    item.code = 10;
                    item.name = "Đã thanh toán";
                    item.des = "Đã thanh toán";
                    item.isdeleted = false;
                    context.sqlStates!.Add(item);
                }

                type = context.sqlStates!.Where(s => s.code == 11).FirstOrDefault();
                if (type == null)
                {
                    SqlState item = new SqlState();
                    item.ID = DateTime.Now.Ticks;
                    item.code = 11;
                    item.name = "Hủy";
                    item.des = "Hủy";
                    item.isdeleted = false;
                    context.sqlStates!.Add(item);
                }

                int rows = await context.SaveChangesAsync();
            }
        }
    }
}
