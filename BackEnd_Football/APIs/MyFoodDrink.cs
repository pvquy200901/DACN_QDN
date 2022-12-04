using BackEnd_Football.Models;
using static BackEnd_Football.APIs.MyNews;

namespace BackEnd_Football.APIs
{
    public class MyFoodDrink
    {

        public MyFoodDrink() { }

        public async Task initAsync()
        {
            using (DataContext context = new DataContext())
            {
                SqlFoodDrink? foodDrink = context.sqlFoodDrinks!.Where(s => s.name.CompareTo("Revive chanh muối") == 0).FirstOrDefault();
                if (foodDrink == null)
                {
                    SqlFoodDrink item = new SqlFoodDrink();
                    item.Id = DateTime.Now.Ticks;
                    item.name = "revive chanh muối";
                    item.price = 10000;
                    item.sellPrice = 15000;
                    item.amount = 100;
                    item.isDelete = false;
                    item.createTime = DateTime.Now.ToUniversalTime();
                    item.updateTime = DateTime.Now.ToUniversalTime();
                    context.sqlFoodDrinks.Add(item);
                }

                foodDrink = context.sqlFoodDrinks!.Where(s => s.name.CompareTo("revive") == 0).FirstOrDefault();
                if (foodDrink == null)
                {
                    SqlFoodDrink item = new SqlFoodDrink();
                    item.Id = DateTime.Now.Ticks;
                    item.name = "revive";
                    item.price = 15000;
                    item.sellPrice = 0;
                    item.amount = 100;
                    item.isDelete = false;
                    item.createTime = DateTime.Now.ToUniversalTime();
                    item.updateTime = DateTime.Now.ToUniversalTime();
                    context.sqlFoodDrinks.Add(item);
                }

                int rows = await context.SaveChangesAsync();
            }

        }

        public class M_foodDrink
        {
            public string name { get; set; } = "";
            public long amount { get; set; }
            public float price { get; set; }
            public float sellPrice { get; set; }
        }

        public async Task<bool> createFDAsync(string token, M_foodDrink m_FoodDrink)
        {

            using (DataContext context = new DataContext())
            {
                SqlFoodDrink? foodDrink = context.sqlFoodDrinks!.Where(s => s.isDelete == false && s.name.CompareTo(m_FoodDrink.name) == 0).FirstOrDefault();
                if (foodDrink != null)
                {
                    return false;
                }

                SqlUserSystem? manager = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (manager == null)
                {
                    return false;
                }
                foodDrink = new SqlFoodDrink();
                foodDrink.Id = DateTime.Now.Ticks;
                foodDrink.name = m_FoodDrink.name;
                foodDrink.amount = m_FoodDrink.amount;
                foodDrink.sellPrice = m_FoodDrink.sellPrice;
                foodDrink.price = m_FoodDrink.price;
                foodDrink.createTime = DateTime.Now.ToUniversalTime();
                foodDrink.updateTime = DateTime.Now.ToUniversalTime();
                foodDrink.userSystem = manager;
                context.sqlFoodDrinks!.Add(foodDrink);

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

        public async Task<bool> editFDAsync(string token, long idFD, M_foodDrink m_FoodDrink)
        {
            using (DataContext context = new DataContext())
            {
                SqlFoodDrink? foodDrink = context.sqlFoodDrinks!.Where(s => s.isDelete == false && s.Id.CompareTo(idFD) == 0).FirstOrDefault();
                if (foodDrink == null)
                {
                    return false;
                }
                SqlUserSystem? manager = context.sqlUserSystems!.Where(s => s.isdeleted == false && s.token.CompareTo(token) == 0).FirstOrDefault();
                if (manager == null)
                {
                    return false;
                }
               
                foodDrink.name = m_FoodDrink.name;
                foodDrink.amount = m_FoodDrink.amount;
                foodDrink.sellPrice = m_FoodDrink.sellPrice;
                foodDrink.price = m_FoodDrink.price;
                foodDrink.updateTime = DateTime.Now.ToUniversalTime();
                foodDrink.userSystem = manager;
                context.sqlFoodDrinks!.Update(foodDrink);

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

        public async Task<bool> deleteFDAsync(string token, long idFD)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            if (idFD == 0)
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlFoodDrink? foodDrink = context.sqlFoodDrinks!.Where(s => s.isDelete == false && s.Id.CompareTo(idFD) == 0).FirstOrDefault();
                if (foodDrink == null)
                {
                    return false;
                }
                foodDrink.isDelete = true;

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


   
        public class ItemFoodDrink
        {
            public string idFD { get; set; } = "";
            public string name { get; set; } = "";
            public long amount { get; set; } = 0;
            public float price { get; set; } = 0;
            public float sellPrice { get; set; } = 0;

        }

        public List<ItemFoodDrink> getListFoodDrink()
        {
            using (DataContext context = new DataContext())
            {
                List<ItemFoodDrink> l_items = new List<ItemFoodDrink>();
                List<SqlFoodDrink> listFD = context.sqlFoodDrinks!.Where(s => s.isDelete == false).ToList();
                foreach (SqlFoodDrink news in listFD)
                {
                    ItemFoodDrink itemFD = new ItemFoodDrink();
                    itemFD.idFD = news.Id.ToString();
                    itemFD.name = news.name;
                    itemFD.amount = news.amount;
                    itemFD.price = news.price;
                    itemFD.sellPrice = news.sellPrice;
                   
                    l_items.Add(itemFD);
                }
                return l_items;
            }
        }

    }
}
