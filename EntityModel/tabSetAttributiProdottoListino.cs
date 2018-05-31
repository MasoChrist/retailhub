namespace EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tabSetAttributiProdottoListino")]
    public partial class tabSetAttributiProdottoListino:baseEntityTable
    {
        [Key]
        [Column(Order = 0)]
        public Guid IDProdottoListino { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid IDGruppoAttributi { get; set; }

        public string Valore { get; set; }

        public virtual tabGruppoAttributi tabGruppoAttributi { get; set; }

        public virtual tabProdottiDiListino tabProdottiDiListino { get; set; }
    }
}
