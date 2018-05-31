using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAuthentication;

namespace RetailHubWeb.Models
{


    public class GridModel<T>
    {

        public GridModel()
        {
            Visibilita = GridMappingAttribute.GetList(typeof(T));
        }
        public readonly List<GridMappingAttribute> Visibilita;

    }

    public  class BaseModel
    {
        public BaseModel(string token)
        {
            User = new AuthenticationService().GetByToken(token);
        }
        public  DTOAuthentication User { get; set; }



    }

    
}