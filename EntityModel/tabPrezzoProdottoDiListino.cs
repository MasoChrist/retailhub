namespace EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tabPrezzoProdottoDiListino")]
    public partial class tabPrezzoProdottoDiListino: baseEntityTable
    {
        [Key]
        [Column(Order = 0)]
        public Guid IDListino { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid IDProdottoDiListino { get; set; }

        public decimal? Prezzo { get; set; }

        public decimal? Maggiorazione { get; set; }

        public bool isPredefinito { get; set; }

        public virtual tabListini tabListini { get; set; }

        public virtual tabProdottiDiListino tabProdottiDiListino { get; set; }
    }
}
