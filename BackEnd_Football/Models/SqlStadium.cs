﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Football.Models
{
    [Table("Stadium")]
    public class SqlStadium
    {
        [Key]
        public long id { get; set; }
        public string name { get; set; } = "";
        public string address { get; set; } = "";
        public string latitude { get; set; } = "";
        public string longitude { get; set; } = "";
        public string contact { get; set; } = "";
        public List<string> images { get; set; } = new List<string>();
        public bool isFinish { get; set; } = false;
        public bool isDelete { get; set; } = false;
        public DateTime createdTime { get; set; }
        public float price { get; set; } = 0;
        public SqlUserSystem? userSystem { get; set; }
        public SqlState? state { get; set; }
    }
}
