using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Authentication;
using DataAccess;
using DataObjects;

namespace TestAngular.Controllers
{
    public class ProdottoController : BaseController<ProdottoService,DTOProdotto,DTOProdottoKey>
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/GetProdottoByDTOSearch")]
       
        public IHttpActionResult GetByDtoSerarch(DTOProdottoSearch search)
        {
            return Ok(Service.GetByDTOSearch(search));
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/DeleteProdottoByID")]
        [ApiAuthorize]
        public IHttpActionResult DeleteProdotto(DTOProdottoKey key)
        {
            return Ok(Service.Delete(key));
        }
    }
}
