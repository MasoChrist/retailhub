using System;
using System.Collections.Generic;
using DataObjects.Core;

namespace DataObjects
{


   
    public class DTOProdotto: BaseGuidIdentifiedDTO,IDTO<GuidKey>
    {
       
        public  string Descrizione { get; set; }
        public string DescrizioneBreve { get; set; }
        public  List<DTOGruppoAttributi> AttributiDisponibili { get; set; }
        public  List<DTOProdottoDiListino> ProdottiDiListino { get; set; }
        /// <summary>
        /// 1 prodotto = 1 Categoria. 1 Categoria = N prodotti
        /// </summary>
        public  DTOCategoria Categoria { get; set; }

       
    }


    public class DTOProdottoSearch: DTOSearchByProprieta
    {
        public  string PartialDescription { get; set; }
        /// <summary>
        /// Cerca tutti i prodotti che hanno tra le categorie padre il nome
        /// </summary>
        public  string PartialCategoria { get; set; }
       /// <summary>
       /// cerca il prodotto associato al prodotto di listino con SKU
       /// </summary>
        public  string PartialSKU { get; set; }

     
    
    }


}
