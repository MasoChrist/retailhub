using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Authentication;
using DataAccess;
using DataObjects;
using TestAngular.Models;

namespace TestAngular.Controllers
{
    public class ProdottoController : BaseController<ProdottoService,DTOProdotto,GuidKey>
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/GetProdottoByDTOSearch")]
       
        public IHttpActionResult GetByDtoSerarch(DTOProdottoSearch search)
        {
            return Ok(Service.GetBySearcher(search).Select(DTOListaProdotti.Map));
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/DeleteProdottoByID")]
        [ApiAuthorize]
        public IHttpActionResult DeleteProdotto(GuidKey key)
        {
            return Ok(Service.Delete(key, CurrentIdentifier));
        }
    }
}
