using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Feeddit.Models
{
    public class Vote
    {

        [Key]
        public int ID { get; set; }
        public long ArticleID { get; set; }
       
        public int UserID { get; set; }

        public int VoteNumber { get; set; }
        [ForeignKey("ArticleID")]
        public virtual Article Article { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}