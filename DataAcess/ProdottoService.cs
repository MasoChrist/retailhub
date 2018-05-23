using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using DataObjects;
using EntityModel;
using MDataObjects;

namespace DataAccess
{
    public class ProdottoService:BaseService<GuidKey, DTOProdotto>,ISearchable<DTOProdottoSearch, DTOProdotto>
    {

       
        protected override bool InnerDelete(GuidKey Chiave)
        {

            if (Chiave == null) return false;
            using (var ctx = new SqlServerEntitites())
            {
                if (!ctx.Products.Any(x => x.ID.Equals(Chiave.ID))) return false;
                var dto = ctx.Products.FirstOrDefault(x => x.ID.Equals(Chiave.ID));
                var prezzoService = new PrezzoService();
                foreach (var prezzo in prezzoService.GetBySearcher(new DTOProdotto{Identifier = Chiave}))
                {
                    prezzoService.Delete(prezzo.Identifier,this.MyIdentifier);
                }

                ctx.Entry(dto).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
                return true;
            }
        }

        protected override GuidKey InnerUpdateOrInsert(DTOProdotto Dato)
        {
            using (var ctx = new SqlServerEntitites())
            {
                var tab = new Products();
                if (!ctx.Products.Any(x => x.ID.Equals(Dato.ID)))
                {
                    Dato.ID = new Guid();
                    ctx.Entry(tab).State = EntityState.Added;

                } 
                else
                {
                    ctx.Entry(tab).State = EntityState.Modified;
                    tab.Description = Dato.Descrizione;
                    tab.ShortDescription = Dato.DescrizioneBreve;
                    tab.SKU = Dato.SKU;
                    tab.ID = Dato.ID;
                }
                ctx.SaveChanges();
                var prezzoService = new PrezzoService();
                foreach (var dtoPrezzo in Dato.PrezziAcquisto.StatedList.Union(Dato.PrezziVendita.StatedList))
                {
                    if(dtoPrezzo.State == ItemState.AddedOrUpdated)
                     prezzoService.UpdateOrInsert( dtoPrezzo.Item, this.MyIdentifier);
                    else
                    {
                        prezzoService.Delete(dtoPrezzo.Item, this.MyIdentifier);
                    }
                }
                return Dato.Identifier;

            }

        }

        public override DTOProdotto GetByID(GuidKey chiave)
        {
            using (var ctx = new SqlServerEntitites())
            {
                return mapTable(ctx.Products.FirstOrDefault(x => x.ID.Equals(chiave.ID)));
            }
        }



        public List<DTOProdotto> GetBySearcher(DTOProdottoSearch searcher)
        {
            return GetByContition(x => x.ShortDescription.Contains(searcher.PartialDescription));
        }


        protected  DTOProdotto mapTable(Products product)
        {
            if (product == null) return null;
            return new DTOProdotto
            {
                ID = product.ID,
                Descrizione = product.Description,
                SKU = product.SKU,
                DescrizioneBreve = product.ShortDescription
            };
        }

        protected List<DTOProdotto> GetByContition(Func<Products, bool> expression)
        {
            using (var ctx = new SqlServerEntitites())
            {
                return ctx.Products.Where(expression).ToList().Select(mapTable).ToList();
            }
        }

       
    }
}
