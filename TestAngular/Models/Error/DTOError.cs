using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailHubWeb.Models.Error
{
    public class DTOError
    {
        public  int ID { get; set; }
        public  string Message { get; set; }

        public  string HelpLink { get; set; }

        public  string RedirectLink { get; set; }
    }
}