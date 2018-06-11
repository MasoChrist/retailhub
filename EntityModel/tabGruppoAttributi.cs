namespace EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tabGruppoAttributi")]
    public partial class tabGruppoAttributi: baseEntityTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tabGruppoAttributi()
        {
            tabProdottiToGruppoAttributi = new HashSet<tabProdottiToGruppoAttributi>();
            tabSetAttributiProdottoListino = new HashSet<tabSetAttributiProdottoListino>();
        }

       

        [Required]
        public string Descrizione { get; set; }

        [Required]
        [StringLength(50)]
        public string DescrizioneBreve { get; set; }

    
    

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tabProdottiToGruppoAttributi> tabProdottiToGruppoAttributi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tabSetAttributiProdottoListino> tabSetAttributiProdottoListino { get; set; }
    }
}
