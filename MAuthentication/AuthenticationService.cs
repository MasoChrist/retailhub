
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Options;

namespace MAuthentication
{

    public static class StringCipher
    {
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase,byte[] saltStringBytes, byte[] ivStringBytes)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  


           
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
           
        }
        public static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
        public static string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

       
    }
    public class AuthenticationService
    {

      protected  Options.ServerOptions Options = new ServerOptions();
        protected string encrypt(string strToEncrypt)
        {
            using (var context = new MAuthentication.RetailHubAuthenticationContext())
            {
                if (string.IsNullOrEmpty( Options.CryptEntropyValues.OptionValue))
                {
                    var bytes = new byte[20];
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                     rng.GetBytes(bytes);
                    Options.CryptEntropyValues.OptionValue = Convert.ToBase64String(bytes);
                }

                if (Options.SaltStringBytes.OptionValue.Length <= 0)
                {
                    Options.SaltStringBytes.OptionValue = StringCipher.Generate256BitsOfRandomEntropy();
                }
                if (Options.IvStringBytes.OptionValue.Length <= 0)
                {
                    Options.IvStringBytes.OptionValue = StringCipher.Generate256BitsOfRandomEntropy();
                }
                return StringCipher.Encrypt(strToEncrypt, Options.CryptEntropyValues.OptionValue, Options.SaltStringBytes.OptionValue,Options.IvStringBytes.OptionValue);

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
                utente.Token = user.Token;
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
                context.SaveChanges();
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
            //--REM
            using (var context = new RetailHubAuthenticationContext())
            {
                if (!context.Users.Any())
                {
                    var tab = new Users();
                    tab.ActivationDate = DateTime.UtcNow;
                    tab.Active = true;
                    tab.ID = Guid.NewGuid();
                    tab.Mail = "mauro@mauro";
                    tab.Password = encrypt("mauro");
                    tab.userName = "mauro";
                    context.Entry(tab).State = EntityState.Added;
                    context.SaveChanges();
                }
               
            }
                //
                var user =
                    GetByCondition(
                        x =>
                            (x.userName.Equals(request.UserName, StringComparison.InvariantCultureIgnoreCase) || x.Mail.Equals(request.UserName, StringComparison.InvariantCultureIgnoreCase)) &&
                            x.Password.Equals(pwd));
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



        public  DTOAuthentication GetByToken(string token)
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
            
            if( (user.LastRequestTime.HasValue && (DateTime.UtcNow - user.LastRequestTime.Value).Days > Options.TokenValidityDate.OptionValue) &&
                (user.CreationDateTime.HasValue && (DateTime.UtcNow - user.CreationDateTime.Value).Days > Options.TokenValidityDate.OptionValue))
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
                if (span.TotalMilliseconds < Options.MilisecondsThresold.OptionValue)
                {
                    return new DTOAuthenticationResponse
                    {
                        Error = new DTOAuthenticationResponseError
                        {
                            Error =
                                $"Riprova in {(Options.MilisecondsThresold.OptionValue - span.Milliseconds / 1000.0)} secondi",
                            ErrorCode = eAuthenticationResponseErrorCode.ThrottleRequest
                        }
                    };

                }
            }
            return new DTOAuthenticationResponse { Token = token };


        }

        //TODO: deve essere parte della chiamata a webapi (provare con  Application_AuthorizeRequest o simili)
        public void SetLastCallDateTime(string token)
        {
            using (var context = new MAuthentication.RetailHubAuthenticationContext())
            {
                var tabuser = context.Users.FirstOrDefault(x => x.Token == token);
                if(tabuser == null) return;
                tabuser.LastRequestDate = DateTime.UtcNow;
                context.Entry(tabuser).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

            }
        }
    }
}