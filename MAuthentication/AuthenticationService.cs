
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Options;

namespace MAuthentication
{
    public class AuthenticationService
    {

      protected  Options.ServerOptions Options = new ServerOptions();
        protected string encrypt(string strToEncrypt)
        {
            using (var context = new MAuthentication.RetailHubAuthenticationContext())
            {
                var plaintext = Encoding.ASCII.GetBytes(strToEncrypt);
                var encrypted = ProtectedData.Protect(plaintext, Options.CryptEntropyValues.OptionValue, DataProtectionScope.LocalMachine);
                return Encoding.ASCII.GetString(encrypted);
            }
        }

    

        protected DTOAuthentication GetByCondition(Func<Users,bool> expression)
        {
            using (var context = new MAuthentication.RetailHubAuthenticationContext())
            {
                var user = context.Users.FirstOrDefault(expression);
                if (user == null) return null;
                return new DTOAuthentication
                {
                    CreationDateTime = user.ActivationDate,
                    LastRequestTime = user.LastRequestDate,
                    Token = user.Token,
                    UserName = user.userName,
                    EncryptedPassword =user.Password,IsActive = user.Active
                };
            }
        }

        private string getToken(DTOAuthentication utente)
        {

            using (var context = new MAuthentication.RetailHubAuthenticationContext())
            {
                var password = utente.EncryptedPassword;
                if (utente == null) return null;

                var user = GetByCondition(x => string.Equals(x.userName, utente.UserName, StringComparison.InvariantCultureIgnoreCase) && x.Password.Equals(password) && x.Active);
                if (user == null) return string.Empty;
                if (string.IsNullOrEmpty(user.Token))
                    generateNewToken(user);
                return user.Token;
            }
        }

        private void generateNewToken(DTOAuthentication user)
        {
            using (var context = new RetailHubAuthenticationContext())
            {
                user.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                var tab = context.Users.FirstOrDefault(x => x.userName.Equals(user.UserName,StringComparison.InvariantCultureIgnoreCase) && x.Password==user.EncryptedPassword);
               // if (tab == null) throw new Exception();//Todo
                tab.Token = user.Token;
                tab.ActivationDate = DateTime.UtcNow;
                tab.LastRequestDate = DateTime.UtcNow;
            }
        }




        public bool DoLogout(string token)
        {
            var dto = GetByToken(token);
            if (dto == null) return false;
         
            return true;
        }
        public DTOAuthenticationResponse DoLogin(DTOAuthenticationRequest request)
        {
            var pwd = encrypt(request.Password);
            var user =
                GetByCondition(
                    x =>
                        (x.userName.Equals(request.UserName, StringComparison.InvariantCultureIgnoreCase)|| x.Mail.Equals(request.UserName, StringComparison.InvariantCultureIgnoreCase)) &&
                        x.Password.Equals(pwd) );
            if (user == null) return new DTOAuthenticationResponse
            {
                Error = new DTOAuthenticationResponseError
                {
                    Error =
                    "Username o Password non validi.",
                    ErrorCode = eAuthenticationResponseErrorCode.UserNotFound
                }
            };
            if (!user.IsActive) return new DTOAuthenticationResponse
            {
                Error = new DTOAuthenticationResponseError
                {
                    Error =
                    "Profilo non Attivo.",
                    ErrorCode = eAuthenticationResponseErrorCode.UserNotActive
                }
            };

            getToken(user);
            return new DTOAuthenticationResponse
            {
                Error = null,
                Token = user.Token
            };

        }



        protected  DTOAuthentication GetByToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;
            return GetByCondition(x => !string.IsNullOrEmpty(x.Token) && x.Token.Equals(token));
        }

        public DTOAuthenticationResponse ValidateToken(string token)
        {
            var user = GetByToken(token);
            if (user == null)
            {
                return new DTOAuthenticationResponse
                {
                    Error = new DTOAuthenticationResponseError
                    {
                        Error = "L'utente non è loggato",
                        ErrorCode = eAuthenticationResponseErrorCode.UserNotLogged
                    }

                };

            }
            using (var context = new MAuthentication.RetailHubAuthenticationContext())
            {
                var tabuser = context.Users.FirstOrDefault(x => x.Token == token);
                tabuser.LastRequestDate = DateTime.UtcNow;
                context.Entry(tabuser).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
             
            }
            if (user.LastRequestTime.HasValue && (DateTime.UtcNow - user.LastRequestTime.Value).Days > Options.TokenValidityDate.OptionValue ||
                user.CreationDateTime.HasValue && (DateTime.UtcNow - user.CreationDateTime.Value).Days > Options.TokenValidityDate.OptionValue)
                return new DTOAuthenticationResponse
                {
                    Error = new DTOAuthenticationResponseError
                    {
                        ErrorCode = eAuthenticationResponseErrorCode.TokenExpired,
                        Error = "E' necessario effettuare il login"
                    }
                };


            if (user.LastRequestTime.HasValue)
            {
                TimeSpan span = DateTime.UtcNow - user.LastRequestTime.Value;
                if (span.Milliseconds < Options.MilisecondsThresold.OptionValue)
                {
                    return new DTOAuthenticationResponse
                    {
                        Error = new DTOAuthenticationResponseError
                        {
                            Error = string.Format("Riprova in {0} secondi", (Options.MilisecondsThresold.OptionValue - span.Milliseconds / 1000.0)),
                            ErrorCode = eAuthenticationResponseErrorCode.ThrottleRequest
                        }
                    };

                }
            }
            return new DTOAuthenticationResponse { Token = token };


        }
    }
}