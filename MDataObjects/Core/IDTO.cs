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
}

  
    
