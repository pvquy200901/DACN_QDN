using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Football.Models
{
    [Table("OrderFD")]
    public class SqlOrderFD
    {
        [Key]
        public long id { get; set; }
        public string code { get; set; } = "";
        public DateTime createOrder { get; set; }
        public bool isDelete { get; set; } = false;
        public bool isFinish { get; set; } = false;
        public float price { get; set; } = 0;
        public SqlUserSystem? userManagerOrder { get; set; }
        public SqlOrderStadium? orderStadium { get; set; }
        public List<SqlFoodDrink> foodDrinks { get; set; } = new List<SqlFoodDrink>();
    }
}
