using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Football.Models
{
    [Table("groupChat")]
    public class GroupChat
    {
        [Key]
        public long id { get; set; }
        public bool isDelete { get; set; } = false;
        public string chat { get; set; } = "";
        public DateTime time { get; set; }
        public SqlTeam? team { get; set; }
        public SqlUser? useName { get; set; }
    }
}
