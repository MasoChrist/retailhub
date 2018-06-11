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

        

        [Required]
        public string Descrizione { get; set; }

        [Required]
        public string DescrizioneBreve { get; set; }
        [Required]
        public  string CodiceArticolo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tabProdottiDiListino> tabProdottiDiListino { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tabProdottiToGruppoAttributi> tabProdottiToGruppoAttributi { get; set; }

        [ForeignKey("Categoria")]
        public  Guid? IdCategoria { get; set; }
        public  tabCategorie Categoria { get; set; }
    }
}
