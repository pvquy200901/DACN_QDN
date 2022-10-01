using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Football.Models
{
    [Table("News")]
    public class News
    {

        [Key]
        public long id { get; set; }
        public string content { get; set; } = "";
        public string contact { get; set; } = "";
        public List<string> images { get; set; } = new List<string>();
        public SqlUser? user { get; set; }
        public SqlUserSystem? manager { get; set; }
        public List<Comment> comments { get; set; } = new List<Comment>();
        public SqlState? state { get; set; }
    }
}
