//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MAuthentication
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        public System.Guid ID { get; set; }
        public string userName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public string Token { get; set; }
        public Nullable<System.DateTime> ActivationDate { get; set; }
        public Nullable<System.DateTime> LastRequestDate { get; set; }
    }
}
