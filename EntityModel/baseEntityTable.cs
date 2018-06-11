using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Diagnostics.CodeAnalysis;

namespace EntityModel
{
    public abstract class baseEntityTable
    {  
        public  Guid ID { get; set; }
        public  Guid? CreatorIDentifier { get; set; }
        public  DateTime? CreationDate { get; set; }

        public  DateTime? LastModifiedDate { get; set; }
        [StringLength(50)]
        public  string LastModifiedBy { get; set; }

        public bool isDeleted { get; set; }

        public virtual  ICollection<tabProperties> Properties { get; set; } = new HashSet<tabProperties>();

    }
}
