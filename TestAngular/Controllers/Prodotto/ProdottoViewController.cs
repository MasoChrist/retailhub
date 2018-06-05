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
    
        public ActionResult Index()
        {
           
         
            return View(new DTOListaProdottiModel( string.Empty));
        }

        [HttpGet]
        public ActionResult Edit(Guid? idProdotto )
        {

            if (idProdotto == null) return View("Edit", "_PopupLayout",(DTOProdotto)null);
            return View("Edit", "_PopupLayout",Service.GetByID(new GuidKey{ID = idProdotto??Guid.Empty}));
        }

        
    }
}