using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Authentication;
using DataAccess;
using DataObjects;
using TestAngular.Controllers;

namespace RetailHubWeb.Controllers
{
    public class CategoriaController :BaseApiController<CategoriaService,DTOCategoria,GuidKey>
    {

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetChildren")]
        [ApiAuthorize]
        public IHttpActionResult GetChildren(Guid? idCategoria = null)
        {
            if (idCategoria == null)
                return Ok(Service.GetBySearcher(new DTOCategoriaSearcher()));
            return Ok(Service.GetBySearcher(new DTOCategoriaSearcherByParent {IDCategoriaPadre = idCategoria.Value}));
        }


    }
}