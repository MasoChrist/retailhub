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


    public class DTOCategoriaSearcher:DTOSearchByProprieta
    {
        public  string PartialNomeCategoria { get; set; }

       
    }

    public class DTOCategoriaSearcherByParent
    {
        public  Guid IDCategoriaPadre { get; set; }
        public  bool AllRoots { get; set; }
    }
}
