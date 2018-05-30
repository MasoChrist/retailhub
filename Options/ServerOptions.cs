using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Options
{
    public class ServerOptions
    {
        public  ServerConfiguration<int> MilisecondsThresold = new ServerConfiguration<int>("MilisecondsThresold",1000);
        public  ServerConfiguration<int> TokenValidityDate = new ServerConfiguration<int>("TokenValidityDate",1);
        public  ServerConfiguration<string> CryptEntropyValues = new ServerConfiguration<string>("CryptEntropyValues", string.Empty); 
        public  ServerConfiguration<Guid> PostazioneCorrente = new ServerConfiguration<Guid>("PostazioneCorrente",Guid.Empty);
        public  ServerConfiguration<byte[]> SaltStringBytes = new ServerConfiguration<byte[]>("SaltStringBytes",new byte[0]);
        public ServerConfiguration<byte[]> IvStringBytes = new ServerConfiguration<byte[]>("IvStringBytes", new byte[0]);

    }
}
