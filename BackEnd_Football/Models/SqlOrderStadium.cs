using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_Football.Models
{
    [Table("OrderStadium")]
    public class SqlOrderStadium
    {

        [Key]
        public long id { get; set; }
        public string code { get; set; } = "";
        public float orderTime  { get; set; }
        public DateTime endTime  { get; set; }
        public DateTime startTime  { get; set; }
        public bool isFinish { get; set; } = false;
        public bool isDelete { get; set; } = false;
        public float price { get; set; } = 0;
        public SqlState? stateOrder { get; set; }
        public SqlUser? userOrder { get; set; }
        public SqlStadium? stadiumOrder { get; set; }
        public SqlUserSystem? userManagerOrder { get; set; }

    }
   
}
