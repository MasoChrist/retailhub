using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace EntityModel
{
    public abstract class baseEntityTable
    {
        public  Guid? CreatorIDentifier { get; set; }
        public  DateTime? CreationDate { get; set; }

        public  DateTime? LastModifiedDate { get; set; }
        [StringLength(50)]
        public  string LastModifiedBy { get; set; }

        public bool isDeleted { get; set; }

    }
}
