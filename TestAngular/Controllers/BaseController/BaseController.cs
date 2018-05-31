using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataObjects;
using EntityModel;
using Options;

namespace RetailHubWeb.Controllers
{
    public abstract class BaseController<TService, TDto, TChiave> : Controller
        where TDto : TChiave, IDTO<IKey>
        where TService : BaseService<TChiave, TDto>


    {
        protected readonly Options.ServerOptions _options = new ServerOptions();

        protected TService Service
        {
            get
            {
                using (var t = new SqlServerEntities())
                {

                    return
                        (TService)
                        Activator.CreateInstance(typeof(TService), _options.PostazioneCorrente.OptionValue, t);

                }

            }
        }

        public BaseController() : base()
        {


        }
    }


}