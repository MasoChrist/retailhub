namespace EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tabProdottiToGruppoAttributi")]
    public partial class tabProdottiToGruppoAttributi: baseEntityTable
    {
        public Guid ID { get; set; }

       

        public Guid IDProdotto { get; set; }

        public Guid IDGruppoAttributi { get; set; }

        public string Valore { get; set; }

        public virtual tabGruppoAttributi tabGruppoAttributi { get; set; }

        public virtual tabProdotti tabProdotti { get; set; }
    }
}
