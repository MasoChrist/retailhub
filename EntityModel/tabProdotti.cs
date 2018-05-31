namespace EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tabProdotti")]
    public partial class tabProdotti: baseEntityTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tabProdotti()
        {
            tabProdottiDiListino = new HashSet<tabProdottiDiListino>();
            tabProdottiToGruppoAttributi = new HashSet<tabProdottiToGruppoAttributi>();
        }

        public Guid ID { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Required]
        public string DescrizioneBreve { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tabProdottiDiListino> tabProdottiDiListino { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tabProdottiToGruppoAttributi> tabProdottiToGruppoAttributi { get; set; }
    }
}
