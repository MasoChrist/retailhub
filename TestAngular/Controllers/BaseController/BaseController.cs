using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;
using DataObjects;
using EntityModel;
using Options;

namespace TestAngular.Controllers
{
    public abstract class BaseController <TService,TDto,TChiave>:ApiController
             where TDto : TChiave, IDTO<IKey>
             where TService: BaseService<TChiave,TDto>


    {
        private readonly Options.ServerOptions _options = new ServerOptions();
       
        protected TService Service
        {
            get
            {
                using (var t = new RetailHubEntities())
                {
                    
                   return (TService) Activator.CreateInstance(typeof(TService), _options.PostazioneCorrente.OptionValue, t);
                    
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

            return Ok(Service.UpdateOrInsert(value, _options.PostazioneCorrente.OptionValue));


        }

        // PUT: api/Base/5
        public IHttpActionResult Put([FromBody]TChiave id, [FromBody]TDto value)
        {
            return Ok(Service.UpdateOrInsert(value, _options.PostazioneCorrente.OptionValue));
        }

        // DELETE: api/Base/5
        public IHttpActionResult Delete([FromBody]TChiave id)
        {
            return Ok(Service.Delete(id, _options.PostazioneCorrente.OptionValue));
        }

       
    }
}
