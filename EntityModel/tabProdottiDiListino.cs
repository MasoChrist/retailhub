namespace EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tabProdottiDiListino")]
    public partial class tabProdottiDiListino: baseEntityTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tabProdottiDiListino()
        {
            tabPrezzoProdottoDiListino = new HashSet<tabPrezzoProdottoDiListino>();
            tabSetAttributiProdottoListino = new HashSet<tabSetAttributiProdottoListino>();
        }

       

        [Required]
        [StringLength(50)]
        public string SKU { get; set; }

        public Guid IDProdotto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tabPrezzoProdottoDiListino> tabPrezzoProdottoDiListino { get; set; }

        public virtual tabProdotti tabProdotti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tabSetAttributiProdottoListino> tabSetAttributiProdottoListino { get; set; }
    }
}
