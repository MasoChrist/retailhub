using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using DataObjects;
using EntityModel;
using LinqKit;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;

namespace DataAccess
{
    public class CategoriaService:BaseService<GuidKey,DTOCategoria>,ISearchable<DTOCategoriaSearcher,DTOCategoria>,ISearchable<DTOCategoriaSearcherByParent,DTOCategoria>
    {
        public CategoriaService(Guid identifier, SqlServerEntities context) : base(identifier, context)
        {
        }

        protected DTOCategoria mapTable(tabCategorie categorie,bool includiPadre, bool includiFiglie)
        {
            if (categorie == null) return null;
            var ret = new DTOCategoria();

            ret.ID = categorie.ID;
            ret.Nome = categorie.Nome;
            if (includiPadre && categorie.IdCategoriaPadre.HasValue)
                ret.CategoriaPadre = GetByID(new GuidKey {ID = categorie.IdCategoriaPadre.Value});
            if (includiFiglie)
            {
                ret.CategorieFiglie=new List<DTOCategoria>();
                foreach (var det in categorie.CategorieFiglie)
                {
                    ret.CategorieFiglie.Add( mapTable(det,false,true));
                }
            }
            return ret;
        }

        public override DTOCategoria GetByID(GuidKey chiave)
        {
            return mapTable(_context.tabCategorie.FirstOrDefault(x => x.ID == chiave.ID), true, true);

        }

        protected override bool InnerDelete(GuidKey chiave)
        {
            var tabl = _context.tabCategorie.FirstOrDefault(x => x.ID == chiave.ID);
            if (tabl == null) return false;
            tabl.isDeleted = true;
            _context.Entry(tabl).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        protected override GuidKey InnerUpdateOrInsert(DTOCategoria categoria)
        {
            //-->TODO: devo trovare un modo per gestire la ricorsione
            var ret = recursiveUpdateOrInsert(categoria);
            _context.SaveChanges();
            return ret;
        }

        private GuidKey recursiveUpdateOrInsert(DTOCategoria categoria)
        {
            if (!_context.tabCategorie.Any(x => x.ID == categoria.ID))
                categoria.ID = new Guid();
            var tab = new tabCategorie();

            tab.ID = categoria.ID;
            tab.Nome = categoria.Nome;
            if (categoria.CategoriaPadre != null)
                tab.IdCategoriaPadre = categoria.CategoriaPadre.ID;
            _context.tabCategorie.AddOrUpdate(tab);
            if (categoria.CategorieFiglie != null)
            {
                foreach (var figlia in categoria.CategorieFiglie)
                {
                    figlia.CategoriaPadre = categoria;
                    recursiveUpdateOrInsert(categoria);
                }
            }
            return categoria;
        }

        public List<DTOCategoria> GetBySearcher(DTOCategoriaSearcher searcher)
        {
            //var predicate = PredicateBuilder.True<tabCategorie>();
            //predicate = predicate.And(x => !x.isDeleted);
            //if(searcher.PartialNomeProprieta.)

            if (searcher == null) searcher = new DTOCategoriaSearcher();
            var predicate = BaseEntityPredicateBuilder.GetBasicPredicate<tabCategorie>(searcher);

            if (!string.IsNullOrEmpty(searcher.PartialNomeCategoria))
            {
                predicate = predicate.And(x => x.Nome.Contains(searcher.PartialNomeCategoria));
            }
            else
            {
                predicate = predicate.And(x => !x.IdCategoriaPadre.HasValue || x.IdCategoriaPadre.Value == x.ID);
            }
            var lst = new List<DTOCategoria>();
            foreach (var dato in _context.tabCategorie.Where(predicate).ToList())
            {
                lst.Add(mapTable(dato,true,false));
            }
            var newLst = new DTOCategoria();
            CreateTreeFromList(lst,newLst);
            return newLst.CategorieFiglie;
        }

        private void CreateTreeFromList(List<DTOCategoria> listaCategorieCompleta,
            DTOCategoria alberoCategorie)
        {
            List<DTOCategoria> listaDaVerificare;
            if (alberoCategorie == null)
            {
                //-->primo giro: tutte le categorie senza un padre
                alberoCategorie = new DTOCategoria();
                listaDaVerificare =
                    listaCategorieCompleta.Where(x => listaCategorieCompleta.All(y => x.ID != y?.CategoriaPadre.ID))
                        .ToList();
            }
            else
            {
                //-->altri giri: tutte le categorie con padre = miaCategoria.id
                listaDaVerificare =
                    listaCategorieCompleta.Where(x => x?.CategoriaPadre.ID == alberoCategorie.ID).ToList();
            }
            alberoCategorie.CategorieFiglie = listaDaVerificare;
            listaCategorieCompleta.RemoveAll(x => listaDaVerificare.Any(y => y.ID == x.ID));
            //->finchè non ho finito
            if (!listaCategorieCompleta.Any()) return;
            foreach (var figlio in alberoCategorie.CategorieFiglie)
            {
                CreateTreeFromList(listaCategorieCompleta,figlio);
            }
        }

        public List<DTOCategoria> GetBySearcher(DTOCategoriaSearcherByParent searcher)
        {
            IQueryable<tabCategorie> tabs;
            if (searcher.AllRoots)
            {
                 tabs = _context.tabCategorie.Where(x => !x.IdCategoriaPadre.HasValue);
            }
            else
            {
                 tabs =
                    _context.tabCategorie.Where(
                        x => x.IdCategoriaPadre.HasValue && x.IdCategoriaPadre == searcher.IDCategoriaPadre);

            }
            var lst = new List<DTOCategoria>();
            foreach (var tab in tabs)
            {
                lst.Add(mapTable(tab,false,false));
            }
            return lst;
        }
    }
}
