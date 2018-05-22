namespace EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Price")]
    public partial class Price
    {
        public Guid ProductID { get; set; }

        public Guid PriceListID { get; set; }

        public decimal Amount { get; set; }

        public decimal Discount { get; set; }

        public Guid ID { get; set; }

        public virtual PriceList PriceList { get; set; }

        public virtual Products Products { get; set; }
    }
}
