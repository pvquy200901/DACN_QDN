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
        public DateTime updateOrder { get; set; }
        public bool isDelete { get; set; } = false;
        public bool isFinish { get; set; } = false;
        public float price { get; set; } = 0;
        public SqlState? stateOrder { get; set; }
        public SqlUserSystem? userManagerOrder { get; set; }
        public SqlOrderStadium? orderStadium { get; set; }
        public List<ItemOrderFoodDrink> listItemFoodDrink { get; set; }
    }

    [Table("ItemOrderFD")]
    public class ItemOrderFoodDrink
    {
        [Key]
        public long id { get; set; } = DateTime.Now.Ticks;
        public string codeOrder { get; set; } = "";
        public long idFD { get; set; }
        public string nameFD { get; set; } = "";
        public float priceFD { get; set; } = 0;
        public int amount { get; set; } = 0;
        public bool isDelete { get; set; } = false;

    }
}
