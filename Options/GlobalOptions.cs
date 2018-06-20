using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Options
{
    public class GlobalOptions
    {
        [Key]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(50)]
        public  string ID { get; set; }
        public  string Valore { get; set; }
    }
}
