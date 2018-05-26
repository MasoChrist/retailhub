using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using DataObjects;
using EntityModel;

namespace DataAccess
{
    public class ProdottoService:BaseService<GuidKey, DTOProdotto>,ISearchable<DTOProdottoSearch, DTOProdotto>
    {
        

        public ProdottoService(Guid identifier, RetailHubEntities context) : base(identifier, context)
        {
        }

        protected override bool InnerDelete(GuidKey Chiave)
        {

            if (Chiave == null) return false;
            using (var ctx = new EntityModel.RetailHubEntities())
            {
                if (!ctx.tabProdotti.Any(x => x.ID.Equals(Chiave.ID))) return false;

                var dto = ctx.tabProdotti.Include(a=>a.tabProdottiToGruppoAttributi).Include(b=>b.tabProdottiDiListino).FirstOrDefault(x => x.ID.Equals(Chiave.ID));
                ctx.tabProdotti.Remove(dto);
                ctx.SaveChanges();
                return true;
            }
        }

        protected override GuidKey InnerUpdateOrInsert(DTOProdotto Dato)
        {
            using (var ctx = new EntityModel.RetailHubEntities())
            {
                var tab = new tabProdotti();
                if (!ctx.tabProdotti.Any(x => x.ID.Equals(Dato.ID)))
                {
                    Dato.ID = new Guid();
                    ctx.Entry(tab).State = EntityState.Added;

                } 
                else
                {
                    ctx.Entry(tab).State = EntityState.Modified;
                    tab.Descrizione = Dato.Descrizione;
                    tab.DescrizioneBreve = Dato.DescrizioneBreve;
                   
                    tab.ID = Dato.ID;


                }
                if(Dato.ProdottiDiListino!=null)
                {

                }

                ctx.SaveChanges();
                return Dato.Identifier;
            }

        }

        public override DTOProdotto GetByID(GuidKey chiave)
        {
            using (var ctx = new EntityModel.RetailHubEntities())
            {
                return mapTable(ctx.tabProdotti.FirstOrDefault(x => x.ID.Equals(chiave.ID)));
            }
        }



        public List<DTOProdotto> GetBySearcher(DTOProdottoSearch searcher)
        {
            return GetByContition(x => x.DescrizioneBreve.Contains(searcher.PartialDescription));
        }


        public DTOProdotto mapTable(tabProdotti product)
        {
            if (product == null) return null;
            return new DTOProdotto
            {
                ID = product.ID,
                Descrizione = product.Descrizione,
                
                DescrizioneBreve = product.DescrizioneBreve
            };
        }

        protected List<DTOProdotto> GetByContition(Func<tabProdotti, bool> expression)
        {
            using (var ctx = new RetailHubEntities())
            {
                return ctx.tabProdotti.Where(expression).ToList().Select(mapTable).ToList();
            }
        }

       
    }
}
