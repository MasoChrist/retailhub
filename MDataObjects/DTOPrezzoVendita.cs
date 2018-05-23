using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace MDataObjects
{

    public class DTOPrezzoKey : IKey
    {
        public  Guid IDListino { get; set; }
        public  Guid IDProdotto { get; set; }
        public int CompareTo(object obj)
        {
            var td = obj as DTOPrezzoKey;
            if (td == null) return -1;
            return td.IDListino.CompareTo(IDListino)  | td.IDProdotto.CompareTo(IDProdotto);
        }
    }
    public class DTOPrezzo: DTOPrezzoKey ,IDTO<DTOPrezzoKey>
    {
        public  decimal Price { get; set; }
        public  decimal Discount { get; set; }
        public  bool IsPredefinito { get; set; }
        public DTOPrezzoKey Identifier =>

            new DTOPrezzoKey
            {
                IDListino = this.IDListino,
                IDProdotto = this.IDProdotto
            };
    }
}
