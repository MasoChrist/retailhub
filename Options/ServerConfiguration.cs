using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Options
{
    

    public class ServerConfiguration<T>
    {
        private static Dictionary<string,string> _optionCache = new Dictionary<string, string>();
        private  string _chiaveImpostazione { get; set; }
        private T _valoreDefault;
        public ServerConfiguration(string chiaveImpostazione,T valoreDefault)
        {
            _chiaveImpostazione = chiaveImpostazione;
            _valoreDefault = valoreDefault;
        }
        public T OptionValue
        {
            get
            {
                if (!_optionCache.TryGetValue(_chiaveImpostazione, out var valore))
                {
                    using (var ctx = new  Options.RetailHubOptions())
                    {
                        var optionValue = ctx.GlobalOptions.FirstOrDefault(x => x.ID.Equals(_chiaveImpostazione, StringComparison.OrdinalIgnoreCase))?.Valore;
                        _optionCache.Add(_chiaveImpostazione,optionValue);
                    }
                }
                if(!_optionCache.TryGetValue(_chiaveImpostazione,out valore))
                    throw  new Exception("Automation error: something really weird just append: no option with name " +_chiaveImpostazione + " found");
                if (valore == null) return _valoreDefault;
                return (T) Convert.ChangeType(valore, typeof(T));

            }
            set
            {
                if (_optionCache.ContainsKey(_chiaveImpostazione))
                    _optionCache[_chiaveImpostazione] = (string) Convert.ChangeType(value, typeof(string));
                using (var ctx = new Options.RetailHubOptions())
                {

                    var tab =
                        ctx.GlobalOptions.FirstOrDefault(
                            x => x.ID.Equals(_chiaveImpostazione, StringComparison.OrdinalIgnoreCase));
                    if (tab == null)
                    {
                        tab = new GlobalOptions();
                        tab.ID = _chiaveImpostazione;
                        ctx.Entry(tab).State = EntityState.Added;
                    }
                    else
                    {
                        ctx.Entry(tab).State = EntityState.Modified;
                    }
                    tab.Valore = (string)Convert.ChangeType(value, typeof(string));


                }
            }
        }


    }
}
