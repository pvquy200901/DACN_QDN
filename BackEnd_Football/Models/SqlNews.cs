using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Football.Models
{
    [Table("News")]
    public class SqlNews
    {
        [Key]
        public long id { get; set; }
        public string code { get; set; } = "";
        public string title { get; set; } = "";
        public string description { get; set; } = "";
        public string shortDes { get; set; } = "";
        public DateTime createdTime { get; set; }
        public List<string> images { get; set; } = new List<string>();
        public SqlUser? user { get; set; }
        public SqlUserSystem? manager { get; set; }
        public List<Comment> comments { get; set; } = new List<Comment>();
        public SqlState? state { get; set; }
    }
}
