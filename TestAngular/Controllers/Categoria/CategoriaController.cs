using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Authentication;
using DataAccess;
using DataObjects;
using RetailHubWeb.Models;
using TestAngular.Controllers;

namespace RetailHubWeb.Controllers
{
    public class CategoriaController :BaseApiController<CategoriaService,DTOCategoria,GuidKey>
    {

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetCategoriaChild")]
        [ApiAuthorize]
        public IHttpActionResult GetChildren(Guid? idCategoria = null)
        {
            List<DTOCategoria> categorie = null;
            if (idCategoria == null)
                categorie = (Service.GetBySearcher(new DTOCategoriaSearcher()));
            categorie = (Service.GetBySearcher(new DTOCategoriaSearcherByParent {IDCategoriaPadre = idCategoria.Value}));
            if (categorie == null) return Ok(new List<DTOAlberoCategorie>());
            return Ok(categorie.Select(x => new DTOAlberoCategorie(x)));

        }


    }
}