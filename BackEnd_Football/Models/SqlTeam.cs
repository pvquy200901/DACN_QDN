using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Football.Models
{
    [Table("Team")]
    public class SqlTeam
    {
        [Key]
        public long id { get; set; }
        public string name { get; set; } = "";
        public string shortName { get; set; } = "";
        public string logo { get; set; } = "";
        public int reputation { get; set; } = 0;
        public string level { get; set; } = "";
        public DateTime createdTime { get; set; }
        public int quantity { get; set; } = 0;
        public string address { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public List<string> imagesTeam { get; set; } = new List<string>();
        public string des { get; set; } = "";
        public List<SqlUser>? user { get; set; }
        public bool isdeleted { get; set; } = false;
        public SqlUser? userCreateTeam { get; set; }

    }
}
