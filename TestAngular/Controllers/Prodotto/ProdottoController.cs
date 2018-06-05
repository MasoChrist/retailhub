using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Authentication;
using DataAccess;
using DataObjects;

using RetailHubWeb.Models;

namespace TestAngular.Controllers
{

    public class ProdottoController : BaseApiController<ProdottoService,DTOProdotto,GuidKey>
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/GetProdottoByDTOSearch")]
        [ApiAuthorize]
        public IHttpActionResult GetByDtoSerarch(DTOProdottoSearch search)
        {
            return Ok(Service.GetBySearcher(search).Select(DTOListaProdotti.Map));
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/DeleteProdottoByID")]
        [ApiAuthorize]
        public IHttpActionResult DeleteProdotto(GuidKey key)
        {
            return Ok(Service.Delete(key, _options.PostazioneCorrente.OptionValue));
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/UpdateOrInsertProdotto")]
        [ApiAuthorize]
        public IHttpActionResult UpdateOrInsert(DTOProdotto prodotto)
        {
            return Ok(Service.UpdateOrInsert(prodotto, _options.PostazioneCorrente.OptionValue));
        }
    }
}
