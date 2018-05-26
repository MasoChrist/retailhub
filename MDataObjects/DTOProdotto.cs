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
       
    }


    public class DTOProdottoSearch
    {
        public  string PartialDescription { get; set; }
    }
}
