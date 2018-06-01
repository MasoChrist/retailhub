using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataObjects;

using RetailHubWeb.Controllers;
using RetailHubWeb.Models;



namespace TestAngular.Controllers
{
 
    public class ProdottoViewController :BaseController<ProdottoService,DTOProdotto,GuidKey>
    {

        // GET: ProdottoView
        public ActionResult Index()
        {
           
         //   var data = new ProdottoService();
            return View(new DTOListaProdottiModel( string.Empty));
        }

        [HttpGet]
        public ActionResult Edit(Guid? idProdotto )
        {
         //   return PartialView(null);
            return PartialView(Service.GetByID(new GuidKey{ID = idProdotto??Guid.Empty}));
        }

        
    }
}