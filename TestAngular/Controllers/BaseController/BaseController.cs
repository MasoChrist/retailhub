using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;
using DataObjects;
using EntityModel;

namespace TestAngular.Controllers
{
    public abstract class BaseController <TService,TDto,TChiave>:ApiController
             where TDto : TChiave, IDTO<IKey>
             where TService: BaseService<TChiave,TDto>


    {
        protected Guid CurrentIdentifier { get; set; }
        protected TService Service
        {
            get
            {
                using (var t = new RetailHubEntities())
                {
                    ///MUST Explode on no value or value not guid
                     CurrentIdentifier = (Guid)Convert.ChangeType(t.TabOpzioniPostazione.First(x => x.ID.Equals("PostazioneCorrente")), typeof(Guid));
                   return (TService) Activator.CreateInstance(typeof(TService), CurrentIdentifier, t);
                }

            }
        }

        public BaseController() : base()
        {

          
        }
        
       

     
        // GET: api/Base/5
        public IHttpActionResult Get([FromBody]TChiave id)
        {

            return Ok(Service.GetByID(id));
        }

        // POST: api/Base
        public IHttpActionResult Post([FromBody]TDto value)
        {

            return Ok(Service.UpdateOrInsert(value,CurrentIdentifier));


        }

        // PUT: api/Base/5
        public IHttpActionResult Put([FromBody]TChiave id, [FromBody]TDto value)
        {
            return Ok(Service.UpdateOrInsert(value, CurrentIdentifier));
        }

        // DELETE: api/Base/5
        public IHttpActionResult Delete([FromBody]TChiave id)
        {
            return Ok(Service.Delete(id, CurrentIdentifier));
        }

       
    }
}
