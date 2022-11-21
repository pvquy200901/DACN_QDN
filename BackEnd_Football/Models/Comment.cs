using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Football.Models
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        public long id { get; set; }
        public bool isDelete { get; set; } = false;
        public string comments { get; set; } = "";
        public DateTime time { get; set; }
        public SqlNews? News { get; set; }
        public SqlUser? useComments { get; set; }
        
    }
}
