
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public interface  ISyncronizable<TDTOChiave,TDTOData>
    {
        string DataTypeIdentifier { get; }
        TDTOData GetByID(TDTOChiave chiave);
        bool Delete(TDTOChiave chiave, Guid creatorIdentifier);
        TDTOChiave UpdateOrInsert(TDTOData dato, Guid creatorIdentifier);
    }
}