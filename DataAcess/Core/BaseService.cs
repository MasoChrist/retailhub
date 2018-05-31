using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ClientNotification;

using DataObjects;

using Newtonsoft.Json;

namespace DataAccess
{
    


    public abstract class BaseService<TDTOChiave ,TDTOData > : ISyncronizable<TDTOChiave, TDTOData> where TDTOData:IDTO<IKey>,TDTOChiave
    {

        //TODO: creatorIdentifier deve essere da qualche parte nei settings del programma e corrispondere a uno dei client nella rete

        
        
        protected Guid _myIdentifier;
        protected EntityModel.SqlServerEntities _context;

        public BaseService(Guid identifier, EntityModel.SqlServerEntities context)
        {
            _myIdentifier = identifier;
            _context = context;

        }

    
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
       

        public string DataTypeIdentifier => typeof(TDTOData).ToString();

        




        public abstract TDTOData GetByID(TDTOChiave chiave);
      

        protected abstract bool InnerDelete(TDTOChiave chiave);
        protected abstract TDTOChiave InnerUpdateOrInsert(TDTOData cato);

        public bool Delete(TDTOChiave chiave,Guid creatorIdentifier)
        {
            var ret = InnerDelete(chiave);
            if (creatorIdentifier != _myIdentifier) return ret;
            if(Orchestrator!=null && ret) Orchestrator.AppendNotification( new DTONotification
            {
                CreationDateTime =  DateTime.UtcNow, CreatorIdentifier = _myIdentifier, Key = 
                JsonConvert.SerializeObject(chiave), NotificationDTOType =  typeof (TDTOData).FullName, NotificationType = NotificationType.Delete, Status = PendingNotificationStatus.Queued
            });
            return ret;
        }

        public TDTOChiave UpdateOrInsert(TDTOData dato,Guid creatorIdentifier)
        {
            var ret = InnerUpdateOrInsert(dato);
            if (creatorIdentifier != _myIdentifier) return ret;
            if (Orchestrator != null ) Orchestrator.AppendNotification(new DTONotification
            {
                CreationDateTime = DateTime.UtcNow,
                CreatorIdentifier = _myIdentifier,
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
