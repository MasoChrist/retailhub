using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace MDataObjects
{

    public class DTOProdottoDiListinoKey : IKey
    {
        public  string SKU { get; set; }
        public int CompareTo(object obj)
        {
            return (obj as DTOProdottoDiListinoKey)?.SKU.CompareTo(SKU) ?? -1;
        }
    }
   
    public class DTOProdottoDiListino : DTOProdottoDiListinoKey,IDTO<DTOProdottoDiListinoKey>
    {
        public DTOProdotto Prodotto { get; set; }

        //->1 per tipo di attibuto
        public  DTOStatedList<DTOValoriAttibuto> Attributi { get; set; }
        //->Univoco
      
        public DTOStatedList<DTOPrezzo> PrezziVendita { get; set; }

        public  List<DTOPrezzo> PrezzoAcquisto { get; set; }
        public DTOStatedList<DTOBarcode> Barcode { get; set; }

        public DTOProdottoDiListinoKey Identifier => new DTOProdottoDiListinoKey {SKU = SKU};

    }
}
