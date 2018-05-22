namespace EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            Price = new HashSet<Price>();
        }

        public Guid ID { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string ShortDescription { get; set; }
        [Required]
        [StringLength(34)]
        ///34 seems the max lenght on Magento
        public string SKU { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Price> Price { get; set; }
    }
}
