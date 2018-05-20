using System;
using DataObjects.Core;

namespace DataObjects
{


    public class DTOProdottoKey:IKey
    {
       
        public int ID { get; set; }
        public int CompareTo(object obj)
        {
            return ID.CompareTo((obj as DTOProdottoKey)?.ID);
        }
    }
    public class DTOProdotto: DTOProdottoKey,IDTO<DTOProdottoKey>
    {
        [GridMapping(Visible = true)]
        public  string Descrizione { get; set; }
        public DTOProdottoKey Identifier=> new DTOProdottoKey{ID = ID};

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
