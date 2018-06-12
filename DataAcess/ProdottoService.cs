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
        

        public ProdottoService(Guid identifier, SqlServerEntities context) : base(identifier, context)
        {
        }

        protected override bool InnerDelete(GuidKey Chiave)
        {

            if (Chiave == null) return false;
            using (var ctx = new EntityModel.SqlServerEntities())
            {
                if (!ctx.tabProdotti.Any(x => x.ID.Equals(Chiave.ID))) return false;

                var dto = ctx.tabProdotti.FirstOrDefault(x => x.ID.Equals(Chiave.ID) && !x.isDeleted);
                //ctx.tabProdotti.Remove(dto);
                if (dto == null) return false;
                dto.isDeleted = true;
                ctx.SaveChanges();
                return true;
            }
        }

        protected override GuidKey InnerUpdateOrInsert(DTOProdotto Dato)
        {
            using (var ctx = new EntityModel.SqlServerEntities())
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
                   

                }
                tab.Descrizione = Dato.Descrizione;
                tab.DescrizioneBreve = Dato.DescrizioneBreve;
                tab.isDeleted = false;
                tab.ID = Dato.ID;

                if (Dato.ProdottiDiListino!=null)
                {

                }

                ctx.SaveChanges();
                return Dato.Identifier;
            }

        }

        public override DTOProdotto GetByID(GuidKey chiave)
        {
            using (var ctx = new EntityModel.SqlServerEntities())
            {
                return mapTable(ctx.tabProdotti.FirstOrDefault(x => x.ID.Equals(chiave.ID)));
            }
        }



        public List<DTOProdotto> GetBySearcher(DTOProdottoSearch searcher)
        {
            var predicate = BaseEntityPredicateBuilder.GetBasicPredicate<tabProdotti>(searcher);
            if (!string.IsNullOrEmpty(searcher.PartialDescription))
                predicate =
                    predicate.And(
                        x =>
                            x.Descrizione.Contains(searcher.PartialDescription) ||
                            x.DescrizioneBreve.Contains(searcher.PartialDescription));
            if (!string.IsNullOrEmpty(searcher.PartialCategoria))
                predicate = predicate.And(x => x.Categoria.Nome.Contains(searcher.PartialCategoria));
            if (!string.IsNullOrEmpty(searcher.PartialSKU)) ;
                //todo: SKU è associato ai prodotti di listino
                
            return GetByContition(predicate);
        }


        public DTOProdotto mapTable(tabProdotti product)
        {
            if (product == null) return null;
            return new DTOProdotto
            {
                ID = product.ID,
                Descrizione = product.Descrizione,
                
                DescrizioneBreve = product.DescrizioneBreve
                //todo: ,Categoria = product.Categoria
               
                //todo:,AttributiDisponibili = product.tabGruppoAttributi
               
            };
        }

        protected List<DTOProdotto> GetByContition(Func<tabProdotti, bool> expression)
        {
            using (var ctx = new SqlServerEntities())
            {
                if (!ctx.tabProdotti.Any())
                {
                    var tab = new tabProdotti();
                    tab.ID = new Guid();
                    tab.Descrizione = "Generico";
                    tab.DescrizioneBreve = "Generico";
                    ctx.Entry(tab).State = EntityState.Added;
                    ctx.SaveChanges();

                }
               
               //TODO: linq Kit è più efficente
                return ctx.tabProdotti.Where(x => !x.isDeleted).Where(expression).ToList().Select(mapTable).ToList();
            }
        }

       
    }
}
