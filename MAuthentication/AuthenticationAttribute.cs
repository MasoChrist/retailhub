using DataAccess;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

namespace Authentication
{
    public class ApiAuthorizeAttribute: System.Web.Http.AuthorizeAttribute
    {
        public static string UserNotLoggedController { get; set; } = "loginview";
        private readonly MAuthentication.AuthenticationService _service = new MAuthentication.AuthenticationService();

        public static bool SkipAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);
         
            return actionContext.ActionDescriptor.GetCustomAttributes<System.Web.Mvc.AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<System.Web.Mvc.AllowAnonymousAttribute>().Any();

        }

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext filterContext)
        {
       
      
            if (SkipAuthorization(filterContext))
            {
                return;
            }

            if (Authorize(filterContext))
            {
                return;
            }
            HandleUnauthorizedRequest(filterContext);
        }

       
    
        private   bool Authorize(HttpActionContext action)
        {
            if (action.Request.Headers.TryGetValues("Token", out var token))
            {

                return _service.ValidateToken(token.First());

            }
            return false;
        }

    }


    public class WebAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        private readonly MAuthentication.AuthenticationService _service =new  MAuthentication.AuthenticationService();
        
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            var actionDescriptor = httpContext.Items["ActionDescriptor"] as ActionDescriptor;
            if (actionDescriptor != null)
            {   if(actionDescriptor.GetCustomAttributes(attributeType: typeof(System.Web.Http.AllowAnonymousAttribute), inherit: true).Any()||
                    actionDescriptor.ControllerDescriptor.GetCustomAttributes(attributeType: typeof(System.Web.Http.AllowAnonymousAttribute), inherit: true).Any())
                    return true;

            }

                var token = httpContext.Request.Cookies["Token"]?.Value;
           if(!string.IsNullOrEmpty(token))
            {
                return _service.ValidateToken(token);
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new
            RouteValueDictionary(new { controller = ApiAuthorizeAttribute.UserNotLoggedController, action ="index" }));
        }
    }
}
