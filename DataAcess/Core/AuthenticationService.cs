using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DataAccess
{
    public class AuthenticationService:BaseService<DTOAuthenticationKey,DTOAuthentication>
    {
        public  static  List<DTOAuthentication> Utenti = new List<DTOAuthentication>();

        private string getToken(DTOAuthentication utente)
        {
            if (utente == null) return null;
            var user = GetByContition(x=> string.Equals(x.UserName ,utente.UserName,StringComparison.InvariantCultureIgnoreCase) && x.Password.Equals(utente.Password)).FirstOrDefault();
            if (user == null) return string.Empty;
            if (string.IsNullOrEmpty(user.Token))
                generateNewToken(user);
            return user.Token;
        }

        private void generateNewToken(DTOAuthentication user)
        {
           user.Token  = new Guid().ToString();
        }

        public AuthenticationService() : base()
        {
            Utenti.Add(new DTOAuthentication{UserName = "mauro",Password = "mauro"});
        }
        protected  List<DTOAuthentication> GetByContition(Func<DTOAuthentication, bool> expression)
        {
            return Utenti.Where(expression).ToList();
        }

        protected override bool InnerDelete(DTOAuthenticationKey chiave)
        {
            Utenti.RemoveAll(x => x.Identifier.CompareTo(chiave) == 0);
            return true;
        }

        protected override DTOAuthenticationKey InnerUpdateOrInsert(DTOAuthentication Dato)
        {
            //-->il match è per username e password
            var datoToUpdate = GetByID(Dato.Identifier);
            if (datoToUpdate == null)
            {
                datoToUpdate = Dato;
                Utenti.Add(datoToUpdate);
            }
            else
            {
                datoToUpdate.UserName = Dato.UserName;
                datoToUpdate.Password = Dato.Password;
                datoToUpdate.Token = Dato.Token;
            }
            return datoToUpdate;
        }

        public bool DoLogout(string token)
        {
            var dto = GetByToken(token);
            if (dto == null) return false;
            dto.Token = string.Empty;
            InnerUpdateOrInsert(dto);
            return true;
        }
        public DTOAuthentication DoLogin(DTOAuthentication chiave)
        {
            var user =
                GetByContition(
                    x =>
                        chiave.UserName.Equals(x.UserName, StringComparison.InvariantCultureIgnoreCase) &&
                        chiave.Password.Equals(x.Password)).FirstOrDefault();
           if (user == null )  return null;
            getToken(user);
            return user;

        }

        public DTOAuthentication GetByToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;
            return GetByContition(x => !string.IsNullOrEmpty(x.Token) &&  x.Token.Equals(token)).FirstOrDefault();
        }
        public bool ValidateToken(string token)
        {

            return GetByToken(token) != null;
        }

        public override DTOAuthentication GetByID(DTOAuthenticationKey chiave)
        {
            return Utenti.FirstOrDefault(x => x.Identifier.Equals(chiave));
        }
    }
}
