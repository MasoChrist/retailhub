using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace MDataObjects
{
    public class DTOAliquotaIva:BaseGuidIdentifiedDTO
    {
        public  decimal Aliquota { get; set; }
        public  string CodiceIva { get; set; }
        public  string Descrizione { get; set; }
    }
}
