using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;
using DataObjects;

namespace TestAngular.Controllers
{
    public abstract class BaseController <TService,TDto,TChiave>:ApiController
             where TDto : TChiave, IDTO<IKey>
             where TService: BaseService<TChiave,TDto>,new()


    {

        protected TService Service => new TService();
        
        // GET: api/Base/5
        public IHttpActionResult Get([FromBody]TChiave id)
        {

            return Ok(Service.GetByID(id));
        }

        // POST: api/Base
        public IHttpActionResult Post([FromBody]TDto value)
        {
            return Ok(Service.UpdateOrInsert(value,Service.MyIdentifier));


        }

        // PUT: api/Base/5
        public IHttpActionResult Put([FromBody]TChiave id, [FromBody]TDto value)
        {
            return Ok(Service.UpdateOrInsert(value, Service.MyIdentifier));
        }

        // DELETE: api/Base/5
        public IHttpActionResult Delete([FromBody]TChiave id)
        {
            return Ok(Service.Delete(id, Service.MyIdentifier));
        }

       
    }
}
