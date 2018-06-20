using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAuthentication
{
    [Table("tabUsers",Schema = "dAuth")]
    public class Users
    {
        [Key]
        public Guid ID { get; set; }

        public DateTime? TokenCreationDateTime { get; set; }
        public  DateTime? LastRequestDateTime { get; set; }
        public  string Token { get; set; }

        public  string UserName { get; set; }
        public  string Password { get; set; }
        public DateTime ActivationDate { get; set; }
        public bool Active { get; set; }
        public string Mail { get; set; }
                  
     
    }
}
