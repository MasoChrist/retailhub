using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using DataAccess;
using DataObjects;
namespace TestAngular.Controllers
{
    [AllowAnonymous]
    public class NavigationBarViewController : Controller
    {
        // GET: NavigationBarView
        public ActionResult Index()
        {
            return PartialView(new NavBarService().GetItems());
        }
    }

   
}