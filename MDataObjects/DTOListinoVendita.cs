using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{

    
    public class DTOListino:BaseGuidIdentifiedDTO
    {
        public string Nome { get; set; }
        public DateTime? DataInizioValidita;
        public DateTime? DataFineValidita;
       
    }
}
