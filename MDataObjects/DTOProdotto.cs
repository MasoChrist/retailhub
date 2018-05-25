using System;
using DataObjects.Core;
using MDataObjects;

namespace DataObjects
{


   
    public class DTOProdotto: BaseGuidIdentifiedDTO,IDTO<GuidKey>
    {
       
        public  string Descrizione { get; set; }
        public string DescrizioneBreve { get; set; }
        public  string SKU { get; set; }
        public  DTOStatedList<DTOProdottoDiListino> ProdottiDiListino { get; set; }

    }


    public class DTOProdottoSearch
    {
        public  string PartialDescription { get; set; }
    }
}
