using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Feeddit.Models
{
    public class Article
    {
        [Key]
        public long ID { get; set; }

       
        public int UserID { get; set; }

       
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "Molimo unesite naziv članka.")]
        [Display(Name = "Headline")]
        public string Title { get; set; }
        [Required]
        //[RegularExpression("^[(http(s)?):\\/\\/(www\\.)?a-zA-Z0-9@:%._\\+~#=]{2,256}\\.[a-z]{2,6}\\b([-a-zA-Z0-9@:%_\\+.~#?&//=]*)$", ErrorMessage = "Uneseni link nije ispravan.")]
        [Url()]
        [Display(Name = "Link")]
        public string ArticleUrl { get; set; }
        [Required(ErrorMessage = "Molimo unesite ime autora.")]
        public string Author { get; set; }

        [Display(Name = "Vote #")]
        public int Votes { get; set; }

        public User User { get; set; }

       
    }


}