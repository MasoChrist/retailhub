using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ISearchable<in TSearchType,TDataType>
    {
        List<TDataType> GetBySearcher(TSearchType searcher);
    }
}
