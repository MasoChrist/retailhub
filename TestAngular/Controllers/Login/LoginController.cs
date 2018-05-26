using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DataAccess;
using DataObjects;

namespace TestAngular.Controllers.Login
{
   
    public class LoginController : ApiController
    {

        protected MAuthentication.AuthenticationService Service = new MAuthentication.AuthenticationService();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/DoLogin")]
        [System.Web.Mvc.AllowAnonymous]
        public IHttpActionResult Login(MAuthentication.DTOAuthenticationRequest authentication)
        {

            return Ok(Service.DoLogin(authentication));
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/DoLogout")]
        
        public IHttpActionResult Logout(string token)
        {
            
            return Ok(Service.DoLogout(token));
        }




    }
}