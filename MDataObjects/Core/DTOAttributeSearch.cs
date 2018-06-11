using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Visto che tutte le entità implementano le proprietàutente, tanto vale che tutti i searcher possano cercare per attributo
    /// </summary>
    public class DTOSearchByProprieta
    {
        public  string PartialNomeProprieta { get; set; }
        public  string PartialValoreProprieta { get; set; }
    }
}
