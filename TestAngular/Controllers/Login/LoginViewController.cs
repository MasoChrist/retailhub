using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Authentication;
using DataAccess;
using DataObjects;
using MAuthentication;


namespace TestAngular.Controllers
{
    [AllowAnonymous]
    public class LoginViewController : Controller
    {
      
      //private AuthenticationService Service=> new AuthenticationService();
        
        [AllowAnonymous]
         public ActionResult Index()

        {      
            var token = ControllerContext.HttpContext.Request.Cookies["Token"]?.Value;
            var service = new AuthenticationService();
            var dto = service.ValidateToken(token);

            return View(dto);
        }

    }
}