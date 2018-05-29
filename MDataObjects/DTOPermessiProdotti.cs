using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace MDataObjects
{

    public class DTOPermessiProdottiKey : IKey
    {
        public  Guid IDProdotto { get; set; }
        public  Guid? Utente { get; set; }
        public  Guid? Client { get; set; }

        public int CompareTo(object obj)
        {
            var cst = obj as DTOPermessiProdottiKey;
            if (cst == null) return -1;
            if (cst.Client != Client) return -1;
            if (cst.Utente != Utente) return -1;
            if (cst.IDProdotto != IDProdotto) return -1;
            return 0;
        }
    }

    public class DTOPermessiProdotti : DTOPermessiProdottiKey, IDTO<DTOPermessiProdottiKey>
    {
        public DTOPermessiProdottiKey Identifier => this;

        public  bool PossoModificare { get; set; }

        public  bool PossoUsare { get; set; }
        //TODO: in teoria vale anche il tipo di documento ( posso inserire in ordine a fornitore, a cliente, in vendita, in rettifiche)
    }
}
