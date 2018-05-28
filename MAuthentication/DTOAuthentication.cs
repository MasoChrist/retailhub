using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAuthentication
{
    public enum eAuthenticationResponseErrorCode:int
    {
        UserNotLogged =1,
        UserNotActive =2,
        TokenExpired =3,
        ThrottleRequest =4,
        UserNotFound =5,
       
    }

    public class DTOAuthentication
    {

            public string Token { get; set; }
            public string UserName { get; set; }
            public string EncryptedPassword { get; set; }
            public DateTime? CreationDateTime { get; set; }
            public DateTime? LastRequestTime { get; set; }
            public bool IsActive { get; set; }
    
    }

    public class DTOAuthenticationRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public class DTOAuthenticationResponse
    {
        public string Token { get; set; }
        public  DTOAuthenticationResponseError Error { get; set; }
    }


    public class DTOAuthenticationResponseError
    {
        public string Error { get; set; }
        public eAuthenticationResponseErrorCode ErrorCode { get; set; } 

    }



}
