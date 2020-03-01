using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Feeddit.Models
{
    public class User
    {
        [Key]
        public int ID
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Korisničko ime je obavezan podatak.")]
        public string UserName
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Lozinka je obavezan podatak.")]
        public string Password
        {
            get;
            set;
        }

        public string Role
        {
            get;
            set;
        }
        public virtual Vote Vote { get; set; }

    }
}