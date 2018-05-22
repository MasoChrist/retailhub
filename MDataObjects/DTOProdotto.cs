using System;
using DataObjects.Core;

namespace DataObjects
{


   
    public class DTOProdotto: BaseGuidIdentifiedDTO,IDTO<GuidKey>
    {
       
        public  string Descrizione { get; set; }
        public string DescrizioneBreve { get; set; }
        public  string SKU { get; set; }

      
        public object GetPropertyByName(string propName)
        {
            return typeof(DTOProdotto).GetProperty(propName).GetValue(this, null);
        }

       
    }


    public class DTOProdottoSearch
    {
        public  string PartialDescription { get; set; }
    }
}
