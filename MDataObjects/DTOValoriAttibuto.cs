using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace MDataObjects
{
    public class DTOValoriAttibuto:BaseGuidIdentifiedDTO //where T: IComparable, IConvertible, IEquatable<T>
    {
        public DTOProdotto Prodotto { get; set; }
        public  DTOTipoAttributo Attributo { get; set; }
        public   object ObjValore { get; set; }
      
    }

    public class DTOValoriAttributo<T> : DTOValoriAttibuto where T : IComparable, IConvertible, IEquatable<T>
    {
        public T Valore
        {
            get { return (T)Convert.ChangeType(ObjValore, typeof(T)); }
            set { ObjValore = value; }
        }

    }

}
