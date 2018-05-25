using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DataObjects
{

    public interface IKey : IComparable
    {
    }
    public interface IDTO<out TKey> where TKey:IKey
    {
        TKey Identifier { get; }

    }
    public class GuidKey : IKey
    {
        public  Guid ID { get; set; }
        public int CompareTo(object obj)
        {      
                return ID.CompareTo((obj as GuidKey)?.ID);  
        }
    }
    public abstract  class BaseGuidIdentifiedDTO: GuidKey,IDTO<GuidKey>,IKey
    {
        public  Guid ID { get; set; }
        public GuidKey Identifier {
            get { return new GuidKey {ID = this.ID}; }
                set { ID = value.ID; } }

      
    }
}

  
    
