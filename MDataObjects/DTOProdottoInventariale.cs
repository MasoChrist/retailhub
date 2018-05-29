using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MDataObjects
{
    public class DTOProdottoInventariale
    {
        public DTOProdottoDiListino Prodotto { get; set; }
        public decimal Giacenza { get; set; }
        public decimal InOrdineAFornitore { get; set; }
        public  decimal InOrdineACliente { get; set; }
        public  decimal ScortaMinima { get; set; }

    }
}
