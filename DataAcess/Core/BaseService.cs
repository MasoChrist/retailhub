using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ClientNotification;

using DataObjects;
using DataObjects.Core;
using Newtonsoft.Json;

namespace DataAccess
{
    


    public abstract class BaseService<TDTOChiave ,TDTOData > : ISyncronizable<TDTOChiave, TDTOData> where TDTOData:IDTO<IKey>,TDTOChiave
    {

        //TODO: creatorIdentifier deve essere da qualche parte nei settings del programma e corrispondere a uno dei client nella rete

        public Guid MyIdentifier { get; set; } = new Guid();
        
        private List<GridMappingAttribute> _visiblita;
        protected ClientNotificatorService Orchestrator { get; set; } = new ClientNotificatorService();

        

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

        public string DataTypeIdentifier => typeof(TDTOData).ToString();

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




        public abstract TDTOData GetByID(TDTOChiave chiave);
      

        protected abstract bool InnerDelete(TDTOChiave chiave);
        protected abstract TDTOChiave InnerUpdateOrInsert(TDTOData cato);

        public bool Delete(TDTOChiave chiave,Guid creatorIdentifier)
        {
            var ret = InnerDelete(chiave);
            if (creatorIdentifier != MyIdentifier) return ret;
            if(Orchestrator!=null && ret) Orchestrator.AppendNotification( new DTONotification
            {
                CreationDateTime =  DateTime.UtcNow, CreatorIdentifier = MyIdentifier, Key = 
                JsonConvert.SerializeObject(chiave), NotificationDTOType =  typeof (TDTOData).FullName, NotificationType = NotificationType.Delete, Status = PendingNotificationStatus.Queued
            });
            return ret;
        }

        public TDTOChiave UpdateOrInsert(TDTOData dato,Guid creatorIdentifier)
        {
            var ret = InnerUpdateOrInsert(dato);
            if (creatorIdentifier != MyIdentifier) return ret;
            if (Orchestrator != null ) Orchestrator.AppendNotification(new DTONotification
            {
                CreationDateTime = DateTime.UtcNow,
                CreatorIdentifier = MyIdentifier,
                Key =
                  JsonConvert.SerializeObject(dato.Identifier),
                NotificationDTOType = typeof(TDTOData).FullName,
                NotificationType = NotificationType.UpdateOrInsert,
                Status = PendingNotificationStatus.Queued
            });
            return ret;
        }

    }
}
