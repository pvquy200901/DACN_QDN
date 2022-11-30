using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Football.Models
{
        [Table("FoodDrink")]
        public class SqlFoodDrink
        {
            [Key]
            public long Id { get; set; }
            public string name { get; set; } = "";
            public long amount { get; set; } = 0;
            public float price { get; set; } = 0;
            public float sellPrice { get; set; } = 0;
            public DateTime createTime { get; set; }
            public DateTime updateTime { get; set; }
            public bool isDelete { get; set; } = false;
            public SqlUserSystem? userSystem { get; set; }
            public SqlState? state { get; set; }

        }
    
}
