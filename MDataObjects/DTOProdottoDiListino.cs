using DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataObjects
{
    /// <summary>
    /// ogni prodotto di listino identifica univocamente un Prodotto con uno specifico set di Attributi.
    /// Questa e' la base per documenti e magazzino
    /// </summary>
    public class DTOProdottoDiListino: BaseGuidIdentifiedDTO
    {
       

        public string SKU { get; set; }
        public List<DTOPrezzo> PrezziVendita { get; set; }
        public List<DTOPrezzo> PrezziAcquisto { get; set; }
        public List<DTOValoreAttributo> Attributi { get; set; }
        

    }
}