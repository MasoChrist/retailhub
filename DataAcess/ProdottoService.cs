using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using DataObjects;

namespace DataAccess
{
    public class ProdottoService:BaseService<DTOProdottoKey,DTOProdotto>
    {
        private static ConcurrentBag<DTOProdotto> Dati { get; set; } = new ConcurrentBag<DTOProdotto>();

        public ProdottoService()
        {
            if (!Dati.Any())
            {
               
                for( var i =0;i<=1000;i++)
                Dati.Add(new DTOProdotto
                {
                    Descrizione = i.ToString() + i.ToString() +i.ToString(),
                    ID = i
                });     
            }
        }
       

        public override bool Delete(DTOProdottoKey Chiave)
        {
            var dato = GetByID(Chiave);
            if (dato == null) return false;
            return Dati.TryTake(out var dto);
        }

        public override DTOProdottoKey UpdateOrInsert(DTOProdotto Dato)
        {
            var dato = GetByID(Dato.Identifier);
            if (dato == null)
            {
                Dati.Add(Dato);
                return dato.Identifier;
            }
            Dato.ID = Dati.Select(x => x.ID).Max() + 1;
            return Dato.Identifier;

        }

        public override List<DTOProdotto> GetByContition(Func<DTOProdotto, bool> expression)
        {
            return Dati.Where(expression).ToList();
        }

        public IEnumerable<DTOProdotto> GetByDTOSearch(DTOProdottoSearch search)
        {
            if (search?.PartialDescription == null)
                return new List<DTOProdotto>();
            return GetByContition(x => x.Descrizione.Contains(search.PartialDescription));
        }
    }
}
