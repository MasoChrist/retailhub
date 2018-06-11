using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    public class tabCategorie:baseEntityTable
    {
        public tabCategorie()
        {
            CategorieFiglie = new HashSet<tabCategorie>();
            Prodotti = new HashSet<tabProdotti>();
        }
        
        public string Nome { get; set; }

        [ForeignKey("CategoriaPadre")]
        public Guid? IdCategoriaPadre { get; set; }

        public virtual tabCategorie CategoriaPadre { get; set; }

        public virtual ICollection<tabCategorie> CategorieFiglie { get; set; }
        public  virtual  ICollection<tabProdotti> Prodotti { get; set; }
        
    }
}
