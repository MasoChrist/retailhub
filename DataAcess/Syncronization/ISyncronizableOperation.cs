using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess
{
    public interface ISyncronizableOperation< TDTOChiave, TDTOData>
    {
        string DTODataIdentifier { get; }
        TDTOData GetByID(TDTOChiave chiave);
        bool Delete(TDTOChiave Chiave);
        TDTOChiave UpdateOrInsert(TDTOData Dato);
    }


}

