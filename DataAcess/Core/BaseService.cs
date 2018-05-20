using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using DataObjects;
using DataObjects.Core;

namespace DataAccess
{
    

    public abstract class BaseService<TDTOChiave ,TDTOData > where TDTOData:IDTO<IKey>,TDTOChiave
    {
        private List<GridMappingAttribute> _visiblita;
        public abstract List<TDTOData> GetByContition(Func<TDTOData, bool> expression);

        public virtual Dictionary<string, object> getValuesByName(TDTOData dato)
        {
            var dic = new Dictionary<string,object>();
            var props = typeof(TDTOData).GetProperties();
            if(props!=null)
                foreach (var prop in props)
                {
                    dic.Add(prop.Name,dato == null? null:typeof(TDTOData).GetProperty(prop.Name).GetValue(dato,null));
                }
            return dic;
        }
        public List<GridMappingAttribute> Visiblita

        {
            get
            {
                if (_visiblita == null) getProperties();
                return _visiblita;
            }
            
        }

        private void getProperties()
        {
            _visiblita = new List<GridMappingAttribute>();
            foreach (var prop in typeof(TDTOData).GetProperties())
            {
                var map = new GridMappingAttribute
               
                {
                    Caption =prop.Name,Name = prop.Name,Visible = false
                };
                foreach (var attr in prop.GetCustomAttributes(true))
                {
                    var tattr = attr as GridMappingAttribute;
                    if (tattr != null)
                    {
                        map.Caption = string.IsNullOrEmpty( tattr.Caption)?map.Name:tattr.Caption;
                        map.Visible = tattr.Visible;
                        break;
                    }

                }
                _visiblita.Add(map);
            }
            _visiblita = _visiblita.OrderBy(x => x.Order).ToList();
        }


        public  List<TDTOData> GetAll()
        {
            return GetByContition(x=> true);
        }

        public TDTOData GetByID(TDTOChiave chiave)
        {
            return GetByContition(x => x.Identifier.Equals(chiave)).FirstOrDefault();
        }

        public abstract bool Delete(TDTOChiave Chiave);
        public abstract TDTOChiave UpdateOrInsert(TDTOData Dato);

        

    }
}
