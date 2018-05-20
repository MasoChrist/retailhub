using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjects
{


    public class DTOAuthenticationKey : IKey
    {
        public  string UserName { get; set; }
        public int CompareTo(object obj)
        {
            return String.Compare(((obj as DTOAuthenticationKey)?.UserName ?? string.Empty ), this.UserName, StringComparison.Ordinal);
        }
    }
    public class DTOAuthentication: DTOAuthenticationKey,IDTO<DTOAuthenticationKey>
    {
       
    
        public string Password { get; set; }
        public  string Token { get; set; }
        public DTOAuthenticationKey Identifier => new DTOAuthenticationKey { UserName = this.UserName};
    }
}
