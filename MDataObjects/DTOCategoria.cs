using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class DTOCategoria:BaseGuidIdentifiedDTO,IDTO<GuidKey>
    {
        public  string Nome { get; set; }
        /// <summary>
        /// il padre dovrebbe essermi più che sufficente per le operazioni di crud
        /// le child saranno gestite eventualmente nella view per avere l'albero
        /// </summary>
        public  DTOCategoria CategoriaPadre { get; set; }
        public  List<DTOCategoria> CategorieFiglie { get; set; }
    }


    public class DTOCategoriaSearcher:DTOSearchByAttribute
    {
        public  string PartialNomeCategoria { get; set; }

        /// <summary>
        /// tutte le categorie che hanno tra i padri il nome categoria
        /// </summary>
        public  string PartialNomeCategoriaPadre { get; set; }
    }
}
