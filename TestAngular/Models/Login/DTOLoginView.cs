using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAuthentication;

namespace RetailHubWeb.Models
{
    public class DTOLoginView:BaseModel
    {
        public DTOLoginView(DTOAuthenticationResponse response) : base(response.Token)
        {
            Response = response;
        }
        public  DTOAuthenticationResponse Response { get; set; }
    }
}