
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

    
namespace EntityModel
{
    [Table("tabProperties")]
    public class tabProperties
    {
        public  Guid ID { get; set; }
        public  string PropertyName { get; set; }
        public  string PropertyValue { get; set; }
        [ForeignKey("BaseEntity")]
        public  Guid? EntityID { get; set; }
        public  virtual  baseEntityTable BaseEntity { get; set; }
    }
}
