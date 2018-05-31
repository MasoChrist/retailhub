namespace EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tabListini")]
    public partial class tabListini: baseEntityTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tabListini()
        {
            tabPrezzoProdottoDiListino = new HashSet<tabPrezzoProdottoDiListino>();
        }

        public Guid ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public DateTime? DataInizioValidita { get; set; }

        public DateTime? DataFineValidita { get; set; }

        public int TipoListino { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tabPrezzoProdottoDiListino> tabPrezzoProdottoDiListino { get; set; }
    }
}
