using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Feeddit.ViewModel
{
    public class ArticleVoting
    {
        public string Title { get; set; } // Article

        public string ArticleUrl { get; set; } // Article

        public string Author { get; set; } // Article

        public int Votes { get; set; } // Article 

        public int VoteNumber { get; set; } // Vote 

        public long ArticleID { get; set; } // Article 

        public int UserID { get; set; } // User 
    }
}