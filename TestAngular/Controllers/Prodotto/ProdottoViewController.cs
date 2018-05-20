using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataObjects;
using DataObjects.Core;


namespace TestAngular.Controllers
{
    
    public class ProdottoViewController : Controller
    {
      
        // GET: ProdottoView
        public ActionResult Index()
        {
           
            var data = new ProdottoService();
            var prodottomodel = new ProdottoViewModel
            {
                Visibilita = data.Visiblita,         
            };
            return View(prodottomodel);
        }
        
    }

    public class ProdottoViewModel
    {
 
        public List<GridMappingAttribute> Visibilita { get; set; }
        public DTOProdottoSearch Searcher { get; set; }
    }
}