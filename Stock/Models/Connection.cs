using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stock.Models
{
    [Table("Connection")]
    public class Connection
    {
        [Key]
        public string ConnectionId { get; set; }
        public string CorrespondingAccountName { get; set; }
    }
}