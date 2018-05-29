using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DataObjects;

namespace MDataObjects
{
    public class DTOTipoAttributo: BaseGuidIdentifiedDTO
    {
        public string Descrizione { get; set; }
        public string DescrizioneBreve { get; set; }
    }
}
