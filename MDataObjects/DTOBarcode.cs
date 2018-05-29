using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace MDataObjects
{
    public class DTOBarcode:BaseGuidIdentifiedDTO
    {
        public  string Barcode { get; set; }
        public  Guid IDProdottoDiListino { get; set; }

    }
}
